namespace DrLightCutterAPITrigger.Model
{
    /// <summary>
    /// 獲取 API 帶入輸入的參數
    /// </summary>
    public class InputParameter
    {
        /// <summary>
        /// 機台編號
        /// </summary>
        public string MachineID { get; set; }
        /// <summary>
        /// 啟始時間
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 終點時間
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// True：白班，False:晚班
        /// </summary>
        public bool Flag { get; set; }
    }
}
