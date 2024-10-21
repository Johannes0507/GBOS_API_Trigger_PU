using DrLightCutterAPITrigger.Helper;
using System.Text;
using Newtonsoft.Json;
using System.Timers;
using DrLightCutterAPITrigger.Model;

namespace DrLightCutterAPITrigger.Services
{
    public class LoginService
    {
        private static readonly HttpClient _httpClient = new();
        private string _accessToken = string.Empty;
        private string _refreshToken = string.Empty;
        private readonly System.Timers.Timer _refreshTimer;
        private const string Username = LoginInfo.Account;
        private const string Password = LoginInfo.PD;

        /// <summary>
        /// 初始化 LoginService 類別。
        /// 設定刷新令牌的計時器。
        /// </summary>
        public LoginService()
        {
            _refreshTimer = new System.Timers.Timer(4.5 * 60 * 60 * 1000); // 4.5 小時
            _refreshTimer.Elapsed += async (sender, e) => await RefreshTokenAsync();
            _refreshTimer.AutoReset = true;
        }

        /// <summary>
        /// 執行用戶登入，發送 POST 請求至登入 URL。
        /// 儲存存取令牌與刷新令牌，並啟動刷新計時器。
        /// </summary>
        public async Task LoginAsync()
        {
            try
            {
                var loginData = new { UserName = Username, Password = Password };
                var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(Urls.CustomerInfo_Login, content);
                response.EnsureSuccessStatusCode();

                var responseData = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                if (responseData != null)
                {
                    _accessToken = responseData.Token;
                    Console.WriteLine($"AccessToken: {_accessToken}");
                }
                else
                {
                    Console.WriteLine("獲取 AccessToken 失敗，回應內容為 null");
                    throw new Exception("獲取 AccessToken 失敗，回應內容為 null");
                }

                _accessToken = responseData.Token;
                _refreshToken = responseData.RefreshToken;

                ConfigureHttpClientAuthorization();
                _refreshTimer.Start();
                Console.WriteLine($"AccessToken： {_accessToken}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"登入失敗: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 使用刷新令牌來刷新存取令牌。
        /// 發送 POST 請求至刷新令牌的 URL 並更新存取令牌。
        /// </summary>
        private async Task RefreshTokenAsync()
        {
            try
            {
                var refreshData = new { RefreshToken = _refreshToken };
                var content = new StringContent(JsonConvert.SerializeObject(refreshData), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(Urls.CustomerInfo_RefreshToken, content);
                response.EnsureSuccessStatusCode();

                var responseData = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()) ?? string.Empty;
                _accessToken = responseData.access_token;
                _refreshToken = responseData.refress_token;

                ConfigureHttpClientAuthorization();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"刷新令牌失敗: {ex.Message}");
                // 處理刷新令牌失敗，例如重新登入或停止服務
            }
        }

        /// <summary>
        /// 設定 HttpClient 的授權標頭。
        /// </summary>
        private void ConfigureHttpClientAuthorization()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
        }

        /// <summary>
        /// 取得 HttpClient 實例。
        /// </summary>
        /// <returns>HttpClient 實例</returns>
        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }
    }
}
