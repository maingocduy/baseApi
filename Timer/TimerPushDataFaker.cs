/*using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using Newtonsoft.Json.Linq;
using ESDManagerApi.Extensions;
using ESDManagerApi.Databases.ESDSoft;
using NLog.LayoutRenderers.Wrappers;

namespace ESDManagerApi.Timers
{
    public class TimerPushDataFaker : IHostedService, IDisposable
    {
        private readonly ILogger<TimerPushDataFaker> _logger;
        private Timer _timer;
        private int TimeLoop = 3 * 60 * 1000; // ms

        public TimerPushDataFaker(ILogger<TimerPushDataFaker> logger)
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
            _logger.LogInformation("Schedule TimerPushDataFaker running.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Schedule TimerPushDataFaker is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void ChangeStateTimer(bool enable)
        {
            _timer ??= new Timer(DoWork, null, Timeout.Infinite, Timeout.Infinite);

            var time_wait = enable ? TimeLoop : Timeout.Infinite;
            _timer.Change(0, time_wait);
        }

        private void DoWork(object state)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            DateTime now = DateTime.Now;

            var _context = ServiceExtension.GetDbContext();
            try
            {
                // Bước 1: Quét dữ liệu từ bảng thiết bị
                var lstDevices = _context.Devices.Include(x => x.GatewayUu).Where(x => x.Status == 1 && !string.IsNullOrEmpty(x.GatewayUuid)).ToList();

                if (lstDevices != null && lstDevices.Count > 0)
                {
                    // Bước 2: Cập nhật vào DeviceHistory
                    /***//*
                     * ID * Giờ hiện tại % 10 >= 8 Thì NG (Tỷ lệ 20%)
                     * Nếu >= 9 thì NG trên 10 phút (Tỷ lệ 10%)
                     **//*

                    var lstNewDeviceHistory = new List<DeviceHistory>();

                    foreach (var device in lstDevices)
                    {
                        var tempValue = (device.Id * now.Hour) % 10;
                        sbyte ngState = 0;

                        Random rd = new Random();

                        if (tempValue >= 8 && tempValue + device.Id % 10 <= now.Minute)
                        {
                            ngState = 1;

                            var ngHistory = _context.NgHistory.Where(x => x.DeviceUuid == device.Uuid && x.TimeNgEnd == null)
                                                              .OrderByDescending(x => x.Id).FirstOrDefault();

                            // Nếu >= 9 thì NG trên 10 phút(Tỷ lệ 10 %)
                            if (tempValue <= 8 && ngHistory != null && ngHistory.TimeNgStart.AddMinutes(8) >= now)
                            {
                                ngState = 0;
                            }
                        }

                        var newDeviceHistory = new DeviceHistory()
                        {
                            DeviceUuid = device.Uuid,
                            GatewayUuid = device.GatewayUuid,
                            TeamUuid = device.TeamUuid,
                            NgStatus = ngState,
                            TimeSynchronized = now,
                            SignalStatus = (sbyte)rd.Next(3),
                            Battery = rd.Next(100),
                            EdsStatic = rd.NextDouble(),
                            State = 1,
                            Status = 0
                        };

                        device.State = newDeviceHistory.State;
                        device.Battery = newDeviceHistory.Battery;
                        device.EdsStatic = newDeviceHistory.EdsStatic;
                        device.GatewayUuid = newDeviceHistory.GatewayUuid;
                        device.TeamUuid = newDeviceHistory.TeamUuid;
                        device.NgStatus = newDeviceHistory.NgStatus;
                        device.SignalStatus = newDeviceHistory.SignalStatus;

                        if (device.GatewayUu != null)
                        {
                            device.GatewayUu.TimeLastOnline = DateTime.Now;
                            device.GatewayUu.State = 1;
                        }

                        _context.SaveChanges();

                        lstNewDeviceHistory.Add(newDeviceHistory);
                    }

                    _context.DeviceHistory.AddRange(lstNewDeviceHistory);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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