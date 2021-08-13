using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ProjectHub.Areas.Identity.IdentityHostingStartup))]
namespace ProjectHub.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}