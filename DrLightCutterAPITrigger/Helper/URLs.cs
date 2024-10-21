namespace DrLightCutterAPITrigger.Helper
{
    public static class Urls
    {
        /// <summary>
        /// 客戶登入的 URL
        /// </summary>
        public const string CustomerInfo_Login = "http://127.0.0.1:3448/api/CustomerInfo/Login";

        /// <summary>
        /// 刷新客戶令牌的 URL
        /// </summary>
        public const string CustomerInfo_RefreshToken = "http://127.0.0.1:3448/api/CustomerInfo/RefreshToken";

        /// <summary>
        /// 獲取設備使用資訊列表的
        /// </summary>
        public const string EquUsaInfo_GetList = "http://127.0.0.1:3448/api/EquUsaInfo/GetList";
        /// <summary>
        /// 獲取設備機台使用統計的 URL
        /// </summary>
        public const string EquUsaInfo_GetMachineUseStatistics = "http://127.0.0.1:3448/api/EquUsaInfo/GetMachineUseStatistics";

        /// <summary>
        /// 獲取狀態資訊列表的 URL
        /// </summary>
        public const string StatuFrInfo_GetList = "http://127.0.0.1:3448/api/IStatuFrlnfo/GetList";
        /// <summary>
        /// 以 DataTable 形式獲取狀態資訊的 URL
        /// </summary>
        public const string StatuFrInfo_GetDataTable = "http://127.0.0.1:3448/api/StatuFrInfo/GetDataTable";
        /// <summary>
        /// 根據 ID 獲取狀態資訊的 URL
        /// </summary>
        public const string StatuFrInfo_GetStatuFrInfoById = "http://127.0.0.1:3448/api/StatuFrInfo/GetStatuFrInfoById";

        /// <summary>
        /// 獲取異常資訊列表的 URL
        /// </summary>
        public const string StatuExInfo_GetList = "http://127.0.0.1:3448/api/StatuExInfo/GetList";
        /// <summary>
        /// 以 DataTable 形式獲取異常資訊的 URL
        /// </summary>
        public const string StatuExInfo_GetDataTable = "http://127.0.0.1:3448/api/StatuExInfo/GetDataTable";
        /// <summary>
        /// 獲取停機原因的 URL
        /// </summary>
        public const string GetStopReasonByDataTable = "http://127.0.0.1:3448/api/StatuExInfo/GetStopReasonByDataTable";

        /// <summary>
        /// 獲取產品詳細資訊列表的 URL
        /// </summary>
        public const string ProductDetailInfo_GetList = "http://127.0.0.1:3448/api/ProductDetailInfo/GetList";
        /// <summary>
        /// 獲取每台機台的 OEE 的 URL
        /// </summary>
        public const string ProductDetailInfo_GetEveryMachineOEE = "http://127.0.0.1:3448/api/ProductDetailInfo/GetEveryMachineOEE";
        /// <summary>
        /// 獲取產品詳細歷史 OEE 的 URL
        /// </summary>
        public const string ProductDetailInfo_GetHistoryOEE = "http://127.0.0.1:3448/api/ProductDetailInfo/GetHistoryOEE";

        /// <summary>
        /// 生产信息（GetList）01
        /// </summary>
        public const string ProductInfo_GetList = "http://127.0.0.1:3448/api/ProductInfo/GetList";
        /// <summary>
        /// 生产信息（GetProductInfoById）02
        /// </summary>
        public const string ProductInfo_GetProductInfoById = "http://127.0.0.1:3448/api/ProductInfo/GetProductInfoById";
        /// <summary>
        /// 生产信息（GetLatelyList）03
        /// </summary>
        public const string ProductInfo_GetLatelyList = "http://127.0.0.1:3448/api/ProductInfo/GetLatelyList";

        /// <summary>
        /// 獲取產品詳細所有資訊列表的 URL
        /// </summary>
        public const string ProductDetailAllInfo_GetList = "http://127.0.0.1:3448/api/ProductDetailAllInfo/GetList";
        /// <summary>
        /// 獲取產品詳細模型資訊的 URL
        /// </summary>
        public const string ProductDetailAllInfo_GetModel = "http://127.0.0.1:3448/api/ProductDetailAllInfo/GetModel";

        /// <summary>
        /// 根據 ID 獲取機器資訊的 URL
        /// </summary>
        public const string MachineInfo_GetMachineInfoById = "http://127.0.0.1:3448/api/MachineInfo/GetMachineInfoById";        
    }
}
 