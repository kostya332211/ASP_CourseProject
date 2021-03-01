using System;
using Chess.Core.Repositories;
using Chess.Web.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ñhess.Infrastructure;

namespace Chess.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<UnitOfWorkOptions>(Configuration.GetSection("UnitOfWork"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<GameHub>();
            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddAuthentication("cookie").AddCookie("cookie", options =>
            {
                options.LoginPath = new PathString("/Login");
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapHub<GameHub>("/gameHub");
            });
        }
    }
}
