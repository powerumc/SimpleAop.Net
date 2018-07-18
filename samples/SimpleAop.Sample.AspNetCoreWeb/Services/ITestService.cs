using System.Threading.Tasks;

namespace SimpleAop.Sample.AspNetCoreWeb.Services
{
    public interface ITestService
    {
        void PrintLog();
        Task<string> GetGoogleHtmlAsync();
    }
}