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
        /// Initializes the ForwardScheduleService class.
        /// </summary>
        /// <param name="forwardService">Instance of ForwardService.</param>
        public ForwardScheduleService(ForwardService forwardService)
        {
            _forwardService = forwardService;
        }

        /// <summary>
        /// Sets up the forwarding timer.
        /// </summary>
        /// <param name="schedule">The schedule configuration for forwarding.</param>
        public void SetForwardSchedule(ForwardSchedule schedule)
        {
            if (_forwardTimer != null)
            {
                _forwardTimer.Stop();
                _forwardTimer.Dispose();
            }

            if (schedule.IsAutoForwardEnabled)
            {
                Console.WriteLine("Forwarding started!");
                _forwardTimer = new System.Timers.Timer(GetIntervalMilliseconds(schedule));
                _forwardTimer.Elapsed += async (sender, e) => await ExecuteForwardAsync();
                _forwardTimer.AutoReset = true;
                _forwardTimer.Start();
            }
        }

        /// <summary>
        /// Executes the forwarding operation.
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
                Console.WriteLine($"An error occurred during scheduled forwarding: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the interval in milliseconds based on the schedule configuration.
        /// </summary>
        /// <param name="schedule">The schedule configuration for forwarding.</param>
        /// <returns>Time interval in milliseconds.</returns>
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
