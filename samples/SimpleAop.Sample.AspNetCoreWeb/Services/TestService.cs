using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SimpleAop.Sample.AspNetCoreWeb.Aspects;

namespace SimpleAop.Sample.AspNetCoreWeb.Services
{
    [LoggingAspect]
    public class TestService : ITestService
    {
        private readonly ILogger<TestService> _logger;

        public TestService(ILogger<TestService> logger)
        {
            _logger = logger;
        }

        public void PrintLog()
        {
            _logger.LogDebug("".PadLeft(80, '-'));
            _logger.LogDebug("This is PrintLog method.");
            _logger.LogDebug("".PadLeft(80, '-'));
        }

        public async Task<string> GetGoogleHtmlAsync()
        {
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync("https://google.com");

            return responseString;
        }
    }
}