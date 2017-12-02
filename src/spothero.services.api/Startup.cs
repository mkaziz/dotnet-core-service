using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpotHero.Services.BusObj.Repositories;
using SpotHero.Services.BusObj.Configuration;
using SpotHero.Services.BusObj.Services;

namespace SpotHero.Services.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IRatesRepository, RatesRepository>();
            services.AddScoped<IJsonFileRetrievalService, JsonFileRetrievalService>();
            services.AddScoped<IJsonFileParserService, JsonFileParserService>();

            services.AddSingleton(Configuration.GetSection("appSettings").Get<AppSettings>() as IAppSettings);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
