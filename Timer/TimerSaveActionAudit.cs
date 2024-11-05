/*using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using Newtonsoft.Json.Linq;
using ESDManagerApi.Extensions;
using ESDManagerApi.Queue;
using ESDManagerApi.Databases.ESDSoft;

namespace ESDManagerApi.Timers
{
    public class TimerSaveActionAudit : IHostedService, IDisposable
    {
        private readonly ILogger<TimerSaveActionAudit> _logger;
        private Timer _timer;

        // Thời gian lặp lại chạy ngầm dịch vụ - 1 phút theo cấu hình
        private int TimeLoop = 1 * 60 * 1000; // ms

        public TimerSaveActionAudit(ILogger<TimerSaveActionAudit> logger)
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
            _logger.LogInformation("Schedule TimerSaveActionAudit running...");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Schedule TimerSaveActionAudit is stopping...");
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

            // Bước 1: Khởi tạo context of CSDL
            var _context = ServiceExtension.GetDbContext();

            try
            {
                // Bước 2: Lấy trong queue manager danh sách cần cập nhật
                var lstActionAudits = ActionQueueManager.dequeue();
                
                if(lstActionAudits != null && lstActionAudits.Count > 0)
                {
                    var lstNewActionAudits = new List<ActionAudit>();

                    // Bước 3: Convert dữ liệu sang kiểu dữ liệu Databse Entity
                    foreach(var audit in lstActionAudits)
                    {
                        var saveActionAudit = new ActionAudit()
                        {
                            UserName = audit.UserName,
                            ActionId = audit.ActionId,
                            Method = audit.Method,
                            Request = audit.Request,
                            State = audit.State,
                            Status = 0
                        };

                        lstNewActionAudits.Add(saveActionAudit);
                    }

                    // Bước 4: Add danh sách vào và gọi lệnh lưu (Lưu theo batchs để tối ưu tốc độ đối với danh sách)
                    _context.ActionAudit.AddRange(lstNewActionAudits);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }finally
            {
                _context.Dispose();
            }

            _timer.Change(TimeLoop, TimeLoop);
        }
    }
}
*/