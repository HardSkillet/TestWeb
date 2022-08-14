using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiddlewareExample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) {
            services.AddTransient<IMessageSender, EmailMessengerSender>();
            services.AddTransient<TimeService>();
        }
        public void Configure(IApplicationBuilder app, IHostEnvironment env , TimeService timeService)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Текущее время: {timeService?.GetTime()}");
            });
        }
    }
}

