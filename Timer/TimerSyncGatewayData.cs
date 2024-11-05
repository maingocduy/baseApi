/*using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using Newtonsoft.Json.Linq;
using ESDManagerApi.Extensions;
using ESDManagerApi.Databases.ESDSoft;
using FirebaseAdmin;
using ESDManagerApi.Firebase;
using NuGet.Protocol.Core.Types;
using System.Net.Mime;
using System;
using Microsoft.TeamFoundation;
using Humanizer;

namespace ESDManagerApi.Timers
{
    public class TimerSyncGatewayData : IHostedService, IDisposable
    {
        private readonly ILogger<TimerSyncGatewayData> _logger;
        private Timer _timer;
        private int TimeLoop = 3 * 60 * 1000; // ms

        public TimerSyncGatewayData(ILogger<TimerSyncGatewayData> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            ChangeStateTimer(true);
            _logger.LogInformation("Schedule TimerSyncGatewayData running.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Schedule TimerSyncGatewayData is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void ChangeStateTimer(bool enable)
        {
            _timer ??= new Timer(DoWork, null, Timeout.Infinite, Timeout.Infinite);

            var time_wait = enable ? TimeLoop : Timeout.Infinite;
            _timer.Change(0, time_wait);
        }
        //Hàm đệ quy để lấy được danh sách team cha
        private async Task FindParentTeams(Team team, List<Team> allTeams, DBContext context)
        {
            if (team.ParentUuid != null)
            {
                //Do không cần thay đổi dữ liệu nên sử dụng AsNoTrack để tăng hiệu suất
                //Lấy ra cha của team có thiết bị NG
                var parentTeams = await context.Team
                    .Include(t => t.User)
                    .AsNoTracking()
                    .Where(t => t.Uuid == team.ParentUuid)
                    .ToListAsync();

                foreach (var parentTeam in parentTeams)
                {
                    if (parentTeam != null)
                    {
                        allTeams.Add(parentTeam);
                        await FindParentTeams(parentTeam, allTeams, context);
                    }
                }
            }
        }
        private async void DoWork(object state)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            DateTime toDay = DateTime.Now.Date;

            var _context = ServiceExtension.GetDbContext();
            try
            {
                // Bước 1: Quét dữ liệu từ bảng lịch sử thiết bị
                var lstNewDevicesHistory = _context.DeviceHistory.Where(x => x.Status == 0).OrderBy(x => x.Id).ToList();

                if (lstNewDevicesHistory != null && lstNewDevicesHistory.Count > 0)
                {
                    var lstCurrentNgDevices = _context.NgHistory.Where(x => x.TimeNgEnd == null).ToList();
                    var lstNgDevicUuids = lstCurrentNgDevices.Select(x => x.DeviceUuid).Distinct().ToList();

                    // Bước 2: Cập nhật các thiết bị gặp tình trạng NG

                    foreach (var device in lstNewDevicesHistory)
                    {
                        if (lstNgDevicUuids.Contains(device.DeviceUuid))
                        {
                            var ngDevice = lstCurrentNgDevices.Where(x => x.DeviceUuid == device.DeviceUuid).FirstOrDefault();

                            if ((ngDevice != null))
                            {
                                // Cập nhật thông tin thiết bị NG
                                ngDevice.SignalStatus = device.SignalStatus;
                                ngDevice.EdsStatic = device.EdsStatic;
                                ngDevice.Battery = device.Battery;

                                TimeSpan difference = DateTime.Now - ngDevice.TimeNgStart;
                                ngDevice.TotalNgMinutes = (int)difference.TotalSeconds; //TODO: Sau đổi lại tên trường TotalNgMinutes DB để dùng seconds

                                // Nếu thiết hết NG
                                if (device.NgStatus == 0)
                                {
                                    ngDevice.TimeNgEnd = DateTime.Now;

                                    lstNgDevicUuids.Remove(device.DeviceUuid);
                                    lstCurrentNgDevices.Remove(ngDevice);
                                }
                                else
                                {
                                    device.NgUuid = ngDevice.Uuid;
                                    if (ngDevice.Status == 0)
                                    {
                                        if (ngDevice.TotalNgMinutes >= 600)
                                        {
                                            ngDevice.Status = 1;
                                            var allTeams = new List<Team>();

                                            //Do không cần thay đổi dữ liệu nên sử dụng AsNoTrack để tăng hiệu suất
                                            var initialTeam = _context.Team
                                                .Include(t => t.User)
                                                .AsNoTracking()
                                                .FirstOrDefault(t => t.Uuid == ngDevice.TeamUuid);

                                            if (initialTeam != null)
                                            {
                                                allTeams.Add(initialTeam);
                                                //Sử dụng đệ quy để lấy ra danh sách team cha
                                                await FindParentTeams(initialTeam, allTeams, _context);

                                                // Lấy tất cả user uuid từ các team cha và team hiện tại
                                                var allUserUuid = allTeams.SelectMany(t => t.User).Select(x => x.Uuid).ToList();

                                                //lấy danh sách username của các uuid đã lấy từ trên
                                                var lstUserName = _context.Account
                                                    .Where(x => allUserUuid.Contains(x.UserUuid) && !string.IsNullOrEmpty(x.FcmToken))
                                                    .Select(x => x.UserName)
                                                    .ToList();

                                                var macnumber = _context.Devices.AsNoTracking().Where(x => x.Uuid == ngDevice.DeviceUuid).Select(x => x.MacNumber).FirstOrDefault();
                                                await FirebaseCloudMessage.SendMulticastMessage(_context, lstUserName, new Models.DataInfo.MessengerDTO()
                                                {
                                                    ContentFireBase = $"Phát hiện thiết bị NG -- {macnumber} của team {initialTeam?.Name}",
                                                    UserSent = "ESD Monitoring",
                                                    TeamUuid = ngDevice.TeamUuid,
                                                    MacNumber = macnumber,
                                                    DeviceUuid = ngDevice.DeviceUuid,
                                                    Content = $"<span>Phát hiện thiết bị NG -- {macnumber} của team <b>{initialTeam?.Name}</b></span>",
                                                });
                                            }
                                            _context.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Nếu thiết bị NG mới
                            if (device.NgStatus == 1)
                            {
                                var newNgDevice = new NgHistory()
                                {
                                    DeviceUuid = device.DeviceUuid,
                                    TeamUuid = device.TeamUuid,
                                    GatewayUuid = device.GatewayUuid,
                                    Battery = device.Battery,
                                    EdsStatic = device.EdsStatic,
                                    SignalStatus = device.SignalStatus,
                                    TimeNgStart = device.TimeCreated,
                                };

                                TimeSpan difference = DateTime.Now - newNgDevice.TimeNgStart;
                                newNgDevice.TotalNgMinutes = (int)difference.TotalSeconds;

                                lstNgDevicUuids.Add(newNgDevice.DeviceUuid);
                                lstCurrentNgDevices.Add(newNgDevice);

                                _context.NgHistory.Add(newNgDevice);
                                _context.SaveChanges();

                                device.NgUuid = newNgDevice.Uuid;
                            }
                        }

                        device.Status = 1;
                    }

                    _context.SaveChanges();
                }

                var now = DateTime.Now;

                // Bước 3: Cập nhật báo cáo theo giờ
                var lastHourBook = _context.HourBook.OrderByDescending(x => x.Id).FirstOrDefault();

                if (lastHourBook is null)
                {
                    lastHourBook = new HourBook()
                    {
                        Uuid = Guid.NewGuid().ToString(),
                        Date = now.Date,
                        Hour = (sbyte)now.Hour,
                        Status = 0
                    };

                    _context.HourBook.Add(lastHourBook);
                    _context.SaveChanges();
                }

                var lastHourTime = lastHourBook.Date.Date.AddHours(lastHourBook.Hour);

                // Thực hiện tổng hợp báo cáo từ thời điểm cuối đến giờ hiện tại
                while (lastHourTime <= now.Date.AddHours(now.Hour))
                {
                    var lstDeviceHistory = _context.DeviceHistory.Include(x => x.NgUu).Where(x => x.TimeSynchronized.Date == lastHourBook.Date.Date
                                                                            && x.TimeSynchronized.Hour == lastHourBook.Hour)
                                                                 .ToList();

                    if (lstDeviceHistory != null && lstDeviceHistory.Count > 0)
                    {
                        var lstDeviceUuids = lstDeviceHistory.Select(x => x.DeviceUuid).Distinct().ToList();
                        var lstNewReports = new List<DailyReport>();

                        foreach (var deviceUuid in lstDeviceUuids)
                        {
                            var lstDeviceByUuid = lstDeviceHistory.Where(x => x.DeviceUuid == deviceUuid).ToList();

                            if (lstDeviceByUuid != null && lstDeviceByUuid.Count > 0)
                            {
                                var ngList = lstDeviceByUuid.Where(x => !string.IsNullOrEmpty(x.NgUuid)).Select(x => x.NgUu).Distinct().ToList();

                                var newReport = _context.DailyReport.Where(x => x.HourBookUuid == lastHourBook.Uuid && x.DeviceUuid == deviceUuid).FirstOrDefault();

                                if (newReport != null)
                                {
                                    newReport.EsdAverage = lstDeviceByUuid.Sum(x => x.EdsStatic) / lstDeviceByUuid.Count;
                                    newReport.NgTimes = ngList?.Count() ?? 0;
                                    newReport.TotalNgMinutes = ngList?.Sum(x => x?.TotalNgMinutes ?? 0) ?? 0;
                                    newReport.LongestNgMinutes = ngList?.MaxBy(x => x.TotalNgMinutes)?.TotalNgMinutes ?? 0;
                                    newReport.OnlineMinutes = 60; //(TODO: Tạm tính)
                                    newReport.SignalAverage = lstDeviceByUuid.Sum(x => x.SignalStatus) / lstDeviceByUuid.Count;
                                }
                                else
                                {
                                    newReport = new DailyReport()
                                    {
                                        DeviceUuid = deviceUuid,
                                        HourBookUuid = lastHourBook.Uuid,
                                        EsdAverage = lstDeviceByUuid.Sum(x => x.EdsStatic) / lstDeviceByUuid.Count,
                                        NgTimes = ngList?.Count() ?? 0,
                                        TotalNgMinutes = ngList?.Sum(x => x?.TotalNgMinutes ?? 0) ?? 0,
                                        LongestNgMinutes = ngList?.MaxBy(x => x.TotalNgMinutes)?.TotalNgMinutes ?? 0,
                                        OnlineMinutes = 60, //(TODO: Tạm tính)
                                        SignalAverage = lstDeviceByUuid.Sum(x => x.SignalStatus) / lstDeviceByUuid.Count,
                                    };

                                    lstNewReports.Add(newReport);
                                }
                            }
                        }

                        _context.DailyReport.AddRange(lstNewReports);

                        if (lastHourTime < now.Date.AddHours(now.Hour))
                        {
                            lastHourBook.Status = 1;
                        }

                        _context.SaveChanges();
                    }

                    if (lastHourTime < now.Date.AddHours(now.Hour))
                    {
                        lastHourBook.Status = 1;
                        _context.SaveChanges();

                        var newHour = new HourBook()
                        {
                            Uuid = Guid.NewGuid().ToString(),
                            Date = lastHourTime.Date,
                            Hour = (sbyte)(lastHourTime.Hour + 1),
                            Status = 0
                        };

                        if (newHour.Date.Date.AddHours(newHour.Hour) < now.Date.AddHours(now.Hour))
                        {
                            newHour.Status = 1;
                        }

                        _context.HourBook.Add(newHour);
                        _context.SaveChanges();

                        lastHourBook = newHour;
                    }

                    lastHourTime = lastHourTime.AddHours(1);
                }

                // Cập nhật trạng thái các thiết bị có trạng thái đang online nhưng không còn cập nhật thông tin sau 5 phút
                var lstOfflineDevies = _context.Devices.Where(x => x.State == 1 && x.TimeLastOnline.AddMinutes(5) < now).ToList();
                var lstOfflineGateway = _context.Gateway.Where(x => x.State == 1 && x.TimeLastOnline.HasValue && x.TimeLastOnline.Value.AddMinutes(5) < now).ToList();
                if (lstOfflineDevies != null && lstOfflineDevies.Count > 0)
                {
                    foreach (var device in lstOfflineDevies)
                    {
                        device.State = 0;
                    }
                }
                if (lstOfflineGateway != null && lstOfflineGateway.Count > 0)
                {
                    foreach (var gateway in lstOfflineGateway)
                    {
                        gateway.State = 0;
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}: {ex.StackTrace}");
            }
            finally
            {
                _context.Dispose();
            }

            _timer.Change(TimeLoop, TimeLoop);
        }
    }
}
*/