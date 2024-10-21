namespace DrLightCutterAPITrigger.Model
{
    /// <summary>
    /// 获取闲置信息（IStatuFrInfoApi）
    /// </summary>
    public class StatusFrInfo
    {
        /// <summary>
        /// 整数 闲置 ID
        /// </summary>
        public int StfId { get; set; }

        /// <summary>
        /// 字符串 机台财编
        /// </summary>
        public string StfMacId { get; set; }

        /// <summary>
        /// ExceptionType 闲置状态代码
        /// </summary>
        public ExceptionType StfCode { get; set; }

        /// <summary>
        /// 字符串 设备机况
        /// </summary>
        public string StfMessage { get; set; }

        /// <summary>
        /// 字符串 设备闲置原因
        /// </summary>
        public string StfReason { get; set; }

        /// <summary>
        /// 整数 当次闲置原因闲置时间(秒)
        /// </summary>
        public int StfFreeReasonTotal { get; set; }

        /// <summary>
        /// 整数 今日相同闲置原因累计时间(秒)
        /// </summary>
        public int StfTodayFreeIdeReaTotal { get; set; }

        /// <summary>
        /// 时间 记录时间(创建时间)
        /// </summary>
        public DateTime StfMarkTime { get; set; }

        /// <summary>
        /// 布尔 是否是白班
        /// </summary>
        public bool StfDayShiftFlag { get; set; }
    }

    public enum ExceptionType
    {
        NoIssue = 0,
        MechanicalFailure = 1,
        ElectricalIssue = 2,
        Maintenance = 3,
        Unknown = 4
    }
}
