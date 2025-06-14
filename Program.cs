using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SignalR_WebApplication.Data.Context;
using SignalR_WebApplication.Hubs;
using SignalR_WebApplication.Service.SignalR.Bugeto.Models.Services;
using SignalR_WebApplication.Tools.ExtensionMethod;

namespace SignalR_WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var mvcBuilder = builder.Services.AddControllersWithViews();

//#if DEBUG
//            mvcBuilder.AddRazorRuntimeCompilation();
//#endif
            builder.Services.AddSignalR();
            builder.AddEFCoreService();
            builder.Services.AddAllService();

            builder.Services.AddMyAuthentication();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}")
                    .WithStaticAssets();
                endpoint.MapHub<SiteChatHub>("/chathub");
                endpoint.MapHub<SupportHub>("/supporthub");
            });

            app.Run();
        }
    }
}
