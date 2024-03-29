using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;
using Microsoft.AspNetCore.Authentication;
using ToDoList.Interfaces;
using ToDoList.Services;
using System.IO;

namespace ToDoList
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
          if (HostingEnvironment.IsProduction()){
            services.AddDbContext<SQLiteDBContext>(db =>
            {
                var contentRoot = Configuration[HostDefaults.ContentRootKey];
                var path = Path.Combine(Path.GetDirectoryName(contentRoot), Configuration.GetConnectionString("ProductionDeployment"));
                db.UseSqlite($"Data Source={path}");
                services.AddDatabaseDeveloperPageExceptionFilter();
            });
          } else {
            services.AddDbContext<SQLiteDBContext>(options =>
            options.UseSqlite(
            Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
          }
            

            services.AddIdentity<ApplicationUser, IdentityRole>
                (options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<SQLiteDBContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            Console.WriteLine("Hosting Enviroment is " + HostingEnvironment.EnvironmentName);
            services.AddControllersWithViews();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ToDoListDbContext, SQLiteDBContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SQLiteDBContext dataContext)
        {
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
                endpoints.MapRazorPages();
            });
        }
    }
}
