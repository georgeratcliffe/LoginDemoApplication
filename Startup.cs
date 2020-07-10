using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginDemoApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using LoginDemoApplication.Repositories;

namespace LoginDemoApplication
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
            //services.AddEntityFrameworkInMemoryDatabase();

            //services.AddDbContext<ApiContext>(c =>
            //    c.UseInMemoryDatabase());

            //services.AddScoped<ApiContext>();


            //Configuration.GetConnectionString("EmployeeDBConnection");
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeDBConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddControllers()
                   .AddNewtonsoftJson(options =>
                       options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<ISessionRepository, DummySessionRepository>();



            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var apicontext = app.ApplicationServices.GetService<ApiContext>();
            //AddTestData(apicontext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        private static void AddTestData(ApiContext apicontext)
        {
            var testdata = new List<Session>()
            {
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,1,1)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,1,5)},
                new Session() {Username = "Paul", Email = "paul@abc.com", Sessiondate= new DateTime(2020,1,24)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,2,5)},
                new Session() {Username = "Paul", Email = "paul@abc.com", Sessiondate= new DateTime(2020,1,1)},
                new Session() {Username = "George", Email = "george@abc.com", Sessiondate= new DateTime(2020,3,19)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,3,1)},
            };

            apicontext.Sessions.AddRange(testdata);
            apicontext.SaveChanges();
        }
    }
}
