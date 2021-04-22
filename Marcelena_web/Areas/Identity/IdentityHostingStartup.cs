using Marcelena_web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Marcelena_web.Areas.Identity.IdentityHostingStartup))]
namespace Marcelena_web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<ShopContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Marcelena_webContextConnection")));

                services.AddHttpContextAccessor();
            });



        }
    }
}