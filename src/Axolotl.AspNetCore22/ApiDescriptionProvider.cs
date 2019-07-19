using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.TestHost;
using System;

namespace Axolotl.AspNetCore22
{
    public class ApiDescriptionProvider<TStartup> : IApiDescriptionGroupCollectionProvider, IDisposable where TStartup : class
    {
        private TestServer testServer;

        public ApiDescriptionGroupCollection ApiDescriptionGroups
        {
            get
            {
                IWebHostBuilder builder = new WebHostBuilder()
                    .UseStartup<TStartup>();
                testServer = new TestServer(builder);
                IApiDescriptionGroupCollectionProvider apiExplorer = testServer.Host.Services.GetService(typeof(IApiDescriptionGroupCollectionProvider)) as IApiDescriptionGroupCollectionProvider
                    ?? throw new ArgumentException("IApiDescriptionGroupCollectionProvider is not provided.");
                return apiExplorer.ApiDescriptionGroups;
            }
        }

        public void Dispose()
        {
            testServer?.Dispose();
        }
    }
}
