using ConfigurationApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        public Startup(IConfiguration config)
        {
            //AppConfiguration = config; - конфигурация по умолчанию

            var builder = new ConfigurationBuilder().
                AddJsonFile("person.json");
            AppConfiguration = builder.Build();
        }

        // свойство, которое будет хранить конфигурацию
        public IConfiguration AppConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddTransient<IMessageSender, EmailMessengerSender>();
            services.AddTransient<TimeService>();
        }
        public void Configure(IApplicationBuilder app)
        {
            var tom = AppConfiguration.Get<Person>();
            app.Run(async (context) =>
            {
                string name = $"<p>Name: {tom.Name}</p>";
                string age = $"<p>Age: {tom.Age}</p>";
                string company = $"<p>Company: {tom.Company?.Title}</p>";
                string langs = "<p>Languages:</p><ul>";
                foreach (var lang in tom.Languages)
                {
                    langs += $"<li><p>{lang}</p></li>";
                }
                langs += "</ul>";

                await context.Response.WriteAsync($"{name}{age}{company}{langs}");
            });
        }
    }
}

