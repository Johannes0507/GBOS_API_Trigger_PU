using DrLightCutterAPITrigger.Helper;
using DrLightCutterAPITrigger.Model;
using System.Timers;

namespace DrLightCutterAPITrigger.Services
{
    public class ForwardScheduleService
    {
        private readonly ForwardService _forwardService;
        private System.Timers.Timer _forwardTimer;

        /// <summary>
        /// 初始化 ForwardScheduleService 類別。
        /// </summary>
        /// <param name="forwardService">ForwardService 實例。</param>
        public ForwardScheduleService(ForwardService forwardService)
        {
            _forwardService = forwardService;
        }

        /// <summary>
        /// 設置轉發計時器。
        /// </summary>
        /// <param name="schedule">轉發的排程設定。</param>
        public void SetForwardSchedule(ForwardSchedule schedule)
        {
            if (_forwardTimer != null)
            {                
                _forwardTimer.Stop();
                _forwardTimer.Dispose();
            }

            if (schedule.IsAutoForwardEnabled)
            {
                Console.WriteLine("開始轉發！");
                _forwardTimer = new System.Timers.Timer(GetIntervalMilliseconds(schedule));
                _forwardTimer.Elapsed += async (sender, e) => await ExecuteForwardAsync();
                _forwardTimer.AutoReset = true;
                _forwardTimer.Start();
            }
        }

        /// <summary>
        /// 執行轉發操作。
        /// </summary>
        private async Task ExecuteForwardAsync()
        {
            try
            {
                var inputParameter = new InputParameter
                {
                    MachineID = "Gbos Automatic E",
                    StartTime = new DateTime(2024, 9, 30),
                    EndTime = new DateTime(2024, 10, 17),
                    Flag = true
                };

                string forwardUrl = "http://cloud-api-url/api/forward";
               
                await _forwardService.FetchAndForwardAsync(Urls.EquUsaInfo_GetList, inputParameter, "");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"定時轉發過程中出現錯誤: {ex.Message}");
            }
        }

        /// <summary>
        /// 根據排程設定取得時間間隔（毫秒）。
        /// </summary>
        /// <param name="schedule">轉發的排程設定。</param>
        /// <returns>時間間隔（毫秒）。</returns>
        private double GetIntervalMilliseconds(ForwardSchedule schedule)
        {
            return schedule.IntervalUnit switch
            {
                TimeUnit.Minutes => schedule.Interval * 60 * 1000,
                TimeUnit.Hours => schedule.Interval * 60 * 60 * 1000,
                TimeUnit.Days => (double)(schedule.Interval * 24 * 60 * 60 * 1000),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
