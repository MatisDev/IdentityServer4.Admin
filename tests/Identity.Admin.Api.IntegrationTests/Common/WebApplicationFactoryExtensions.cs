using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Identity.Admin.Api.Configuration.Test;

namespace Identity.Admin.Api.IntegrationTests.Common
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient SetupClient(this WebApplicationFactory<StartupTest> fixture)
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };

            return fixture.WithWebHostBuilder(
                builder => builder
                    .UseStartup<StartupTest>()
                    .ConfigureTestServices(services => { })
            ).CreateClient(options);
        }
    }
}