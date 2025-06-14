using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SignalR_WebApplication.Data.Context;
using SignalR_WebApplication.Service;
using SignalR_WebApplication.Service.SignalR.Bugeto.Models.Services;

namespace SignalR_WebApplication.Tools.ExtensionMethod
{
    public static class InjectService
    {
        public static IServiceCollection AddAllService(this IServiceCollection services)
        {
            services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
        public static IServiceCollection AddMyAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(option =>
                {
                    option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(Options =>
                {
                    Options.LoginPath = "/Account/login";
                });
            return services;
        }

        public static WebApplicationBuilder AddEFCoreService(this WebApplicationBuilder builder)
        {
            string conectionString = builder.Configuration["ConnectionStrings:LocalDb"] ?? "Data Source=.;Initial Catalog=SignalRDB;Integrated Security=True;";

            builder.Services.AddDbContext<AppDbContext>(_ => _.UseSqlServer(conectionString));
            return builder;
        }
    }
}
