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
        /// Initializes the ForwardService class, using the HttpClient from the LoginService to send requests.
        /// </summary>
        public ForwardService(LoginService loginService, ILogger<ForwardService> logger)
        {
            _httpClient = loginService.GetHttpClient();
            _logger = logger;
        }

        /// <summary>
        /// Retrieves data from the specified URL and forwards it to another URL.
        /// </summary>
        /// <param name="originalUrl">The URL of the original data.</param>
        /// <param name="inputParameter">The input parameters, including machine ID, start time, end time, etc.</param>
        /// <param name="forwardUrl">The target URL to forward to. If empty, no forwarding is performed.</param>
        public async Task FetchAndForwardAsync(string originalUrl, InputParameter inputParameter, string forwardUrl = "")
        {
            try
            {
                // Build the request URL with the parameters in the query string
                string requestUrl = $"{originalUrl}?MachineId={inputParameter.MachineID}&StartDate={inputParameter.StartTime:yyyy/MM/dd}&EndDate={inputParameter.EndTime:yyyy/MM/dd}&Flag={inputParameter.Flag}";

                _logger.LogInformation("Preparing to fetch data from {OriginalUrl}. Request URL: {RequestUrl}", originalUrl, requestUrl);

                // Fetch data from the original URL
                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Successfully retrieved data from {OriginalUrl}: {Data}", originalUrl, data);

                // If a forward URL is provided, forward the data
                if (!string.IsNullOrWhiteSpace(forwardUrl))
                {
                    _logger.LogInformation("Preparing to forward data to {ForwardUrl}", forwardUrl);

                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var forwardResponse = await _httpClient.PostAsync(forwardUrl, content);
                    forwardResponse.EnsureSuccessStatusCode();

                    _logger.LogInformation("Data successfully forwarded to {ForwardUrl}", forwardUrl);
                }
                else
                {
                    _logger.LogWarning("No forwarding URL provided, skipping forwarding operation.");
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "HTTP request error: {Message}", httpEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during processing or forwarding data: {Message}", ex.Message);
            }
        }
    }
}
