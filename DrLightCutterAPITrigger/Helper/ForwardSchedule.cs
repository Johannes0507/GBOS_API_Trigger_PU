namespace DrLightCutterAPITrigger.Helper
{
    /// <summary>
    /// Setting time parameter class
    /// </summary>
    public class ForwardSchedule
    {
        /// <summary>
        /// Setted interval.
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        ///time interval unit (minutes, hours, days).
        /// </summary>
        public TimeUnit IntervalUnit { get; set; }
        /// <summary>
        /// bool value of auto forward enabled.
        /// </summary>
        public bool IsAutoForwardEnabled { get; set; }
    }

    /// <summary>
    /// time unit enumeration.
    /// </summary>
    public enum TimeUnit
    {
        Minutes,
        Hours,
        Days
    }
}
