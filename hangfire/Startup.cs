using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hangfire.Infraestructure.Hangfire;
using hangfire.Services;
using hangfire.Services.Interfaces;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace hangfire
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
            services.AddHangfire(configuration => configuration
                                   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                   .UseSimpleAssemblyNameTypeSerializer()
                                   .UseRecommendedSerializerSettings()
                                   .UseMemoryStorage());
            services.AddHangfireServer();
            services.AddTransient<ISendMailServices, SendMailServices>();
            services.AddTransient<IHangFireServices, HangFireServices>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RecurringJob.AddOrUpdate<IHangFireServices>(hangfire => hangfire.ScheduleJob(), Cron.Weekly);
        }
    }
}
