using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace XeroProductsTests.IntegrationTests
{
    /// <summary>
    /// integration test to test working of the APIs 
    /// </summary>
    [TestClass]
    public class IntegrationTest
    {
       [TestMethod]
        public async Task IntegrationTest_Product()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<XeroProducts.Startup>();

                    // Specify the environment
                    webHost.UseEnvironment("Development");

                    webHost.Configure(app => app.Run(async ctx => await ctx.Response.WriteAsync("Hello World!")));

                    
                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            // Act
            var response = await client.GetAsync("/api/Products");
            
            // Assert
            Assert.AreEqual(response.StatusCode,HttpStatusCode.OK);
            
        }

        [TestMethod]
        public async Task IntegrationTest_ProductOptions()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<XeroProducts.Startup>();

                    // Specify the environment
                    webHost.UseEnvironment("Development");

                    webHost.Configure(app => app.Run(async ctx => await ctx.Response.WriteAsync("Hello World!")));


                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            // Act
            var response = await client.GetAsync("/api/Products/C9E3BC68-497B-4439-BBD0-92570A7670F4/options");

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

        }
    }
}
