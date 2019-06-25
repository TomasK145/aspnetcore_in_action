using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseWelcomePage(); //middleware pre welcome stranku
            //app.UseWelcomePage("/"); //welcomepage middleware handluje vsetky requesty na root url kedze je definovana skor ako mvc middleware/// request URL "~/" sa nikdy nedostane na spracovanie do MVC middlewareu
            //TIP You should always consider the order of middleware when adding to the Configure method. Middleware added earlier in the pipeline will run(and potentially return a response) before middleware added later.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //nepouzivat na PROD (kvoli security)
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseStatusCodePages(); //error stranka pre Status code errory (pr. 404 zobrazi --> Status Code: 404; Not Found)
            //app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}"); //umoznuje zobrazovat custom stranku napr pre 404 status code

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
