using Microsoft.Extensions.DependencyInjection;

namespace Pan.Web.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.UserPanWeb();
        }
    }
}