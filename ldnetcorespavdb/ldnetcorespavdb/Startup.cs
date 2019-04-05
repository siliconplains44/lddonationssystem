using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

using ldvdbbusinesslogic;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace netcoreldspaframework
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var appConfig = new AppConfiguration();

            JObject settingsFileJson = JObject.Parse(File.ReadAllText("ldsettings.json"));                        
            appConfig.SendGridApiKey = settingsFileJson["SendGridApiKey"].ToObject<string>();
            appConfig.TwilioaccountSid = settingsFileJson["TwilioaccountSid"].ToObject<string>();
            appConfig.TwilioAuthToken = settingsFileJson["TwilioAuthToken"].ToObject<String>();
            appConfig.FromEmailAddress = settingsFileJson["fromemailaddress"].ToObject<String>();
            appConfig.FromSmsPhoneNumber = settingsFileJson["fromsmsphonenumber"].ToObject<String>();

            var bl = new CustomBusinessLogic(null);
            bl.SetConfiguration(appConfig);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {           
            services.AddMvc();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");                
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
