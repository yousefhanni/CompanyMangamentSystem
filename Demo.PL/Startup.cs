using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.PL.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// Any thing we want it to work with Dependency Enjection we put it  here
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();  //Add MVC Services 

            services.AddDbContext<MVCAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            ///AddTransient()
            ///if you ask from ClR to create object that implement interface IDepartmentRepository
            ///=> CLR will create object but If you ask object again at the same request
            ///=> CLR will create new and diffrent object from DepartmentRepository
            ///( per operations that will exist within request)
            ///Examples on Services that work with life time Transient that you sure that you will need one object at one request
            ///=>like=> Mappping, convert from model to view model or from view model to model

            ///AddScoped()
            ///if you ask from ClR to create object that implement interface IDepartmentRepository
            ///=> CLR will create object , If you ask object again at the same request
            ///=> CLR will not created new and diffrent object,CLR will give you the same old object
            ///(per request at all)

            ///AddSingleton()
            ///if you ask from ClR to create object that implement interface IDepartmentRepository
            ///=> CLR will create object , If you ask object again at diffrent requests
            ///CLR Will create the same object that was exist at the first request 
            ///Examples on Services that work with life time Singleton is Caching services and Log Services that make Exception

            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            //
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseHttpsRedirection(); //If end user Make Request Http Will Redirec to Https
            app.UseStaticFiles();     //If needed to using any file from files that are at WWWroot

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

/*
What is asynchronous vs synchronous programming? 

Asynchronous : is a non-blocking architecture,
so the execution of one task isn't dependent on another.Tasks can run simultaneously.
Synchronous  : is a blocking architecture, 
so the execution of each operation depends on completing the one before it.

 */
