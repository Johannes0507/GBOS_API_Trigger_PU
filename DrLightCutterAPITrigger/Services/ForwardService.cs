using DrLightCutterAPITrigger.Model;
using System.Text;
using Microsoft.Extensions.Logging;

namespace DrLightCutterAPITrigger.Services
{
    public class ForwardService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ForwardService> _logger;

        /// <summary>
        /// 初始化 ForwardService 類別，使用 LoginService 的 HttpClient 來發送請求。
        /// </summary>
        public ForwardService(LoginService loginService, ILogger<ForwardService> logger)
        {
            _httpClient = loginService.GetHttpClient();
            _logger = logger;
        }

        /// <summary>
        /// 從指定的 URL 獲取資料後，轉發至另一個 URL。
        /// </summary>
        /// <param name="originalUrl">原始資料的 URL。</param>
        /// <param name="inputParameter">輸入參數，包括機器 ID、開始時間、結束時間等。</param>
        /// <param name="forwardUrl">需要轉發的目標 URL，若為空則不轉發。</param>
        public async Task FetchAndForwardAsync(string originalUrl, InputParameter inputParameter, string forwardUrl = "")
        {
            try
            {
                // 構建請求 URL，將參數帶入 URL 中
                string requestUrl = $"{originalUrl}?MachineId={inputParameter.MachineID}&StartDate={inputParameter.StartTime:yyyy/MM/dd}&EndDate={inputParameter.EndTime:yyyy/MM/dd}&Flag={inputParameter.Flag}";

                _logger.LogInformation("準備從 {OriginalUrl} 獲取資料，請求 URL：{RequestUrl}", originalUrl, requestUrl);

                // 從原始 URL 獲取資料
                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("成功從 {OriginalUrl} 獲取數據：{Data}", originalUrl, data);

                // 如果有轉發 URL，將資料轉發
                if (!string.IsNullOrWhiteSpace(forwardUrl))
                {
                    _logger.LogInformation("準備將數據轉發至 {ForwardUrl}", forwardUrl);

                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var forwardResponse = await _httpClient.PostAsync(forwardUrl, content);
                    forwardResponse.EnsureSuccessStatusCode();

                    _logger.LogInformation("資料成功轉發到 {ForwardUrl}", forwardUrl);
                }
                else
                {
                    _logger.LogWarning("沒有設定轉發 URL，跳過轉發操作。");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "HTTP 請求錯誤：{Message}", httpEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "處理或轉發資料時發生錯誤：{Message}", ex.Message);
            }
        }
    }
}
