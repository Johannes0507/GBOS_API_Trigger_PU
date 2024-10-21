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
        /// Initializes the LoginService class.
        /// Sets up the refresh token timer.
        /// </summary>
        public LoginService()
        {
            _refreshTimer = new System.Timers.Timer(4.5 * 60 * 60 * 1000); // 4.5 hours
            _refreshTimer.Elapsed += async (sender, e) => await RefreshTokenAsync();
            _refreshTimer.AutoReset = true;
        }

        /// <summary>
        /// Performs user login by sending a POST request to the login URL.
        /// Stores the access token and refresh token, and starts the refresh timer.
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
                    Console.WriteLine("Failed to obtain AccessToken, response content is null");
                    throw new Exception("Failed to obtain AccessToken, response content is null");
                }

                _accessToken = responseData.Token;
                _refreshToken = responseData.RefreshToken;

                ConfigureHttpClientAuthorization();
                _refreshTimer.Start();
                Console.WriteLine($"AccessToken: {_accessToken}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Uses the refresh token to refresh the access token.
        /// Sends a POST request to the refresh token URL and updates the access token.
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
                Console.WriteLine($"Failed to refresh token: {ex.Message}");
                // Handle token refresh failure, such as re-login or stopping the service
            }
        }

        /// <summary>
        /// Configures the HttpClient authorization header.
        /// </summary>
        private void ConfigureHttpClientAuthorization()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
        }

        /// <summary>
        /// Retrieves the HttpClient instance.
        /// </summary>
        /// <returns>HttpClient instance</returns>
        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }
    }
}
