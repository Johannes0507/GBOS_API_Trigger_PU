namespace DrLightCutterAPITrigger.Helper
{
    /// <summary>
    /// 設定時間的參數類別
    /// </summary>
    public class ForwardSchedule
    {
        /// <summary>
        /// 設定的時間間隔。
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 時間間隔的單位（分鐘、小時、天）。
        /// </summary>
        public TimeUnit IntervalUnit { get; set; }
        /// <summary>
        /// 是否啟用自動轉發。
        /// </summary>
        public bool IsAutoForwardEnabled { get; set; }
    }

    /// <summary>
    /// 時間單位的枚舉。
    /// </summary>
    public enum TimeUnit
    {
        Minutes,
        Hours,
        Days
    }
}
