using KlementWeb.Business.Classes;
using KlementWeb.Business.Interfaces;
using KlementWeb.Business.Managers;
using KlementWeb.Business.Services;
using KlementWeb.Data;
using KlementWeb.Data.Interfaces;
using KlementWeb.Data.Models;
using KlementWeb.Data.Repositories;
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

namespace KlementWeb
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());
            services.AddIdentity<WebUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequiredLength = 10; 
            }) .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddScoped<IEmail, Email>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleManager, ArticleManager>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager, UserManager<WebUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("ArticleIndex", "Clanky", defaults: new { controller = "Article", action = "Index" });
                endpoints.MapControllerRoute("ArticleDetail", "Clanky/{url}", defaults: new { controller = "Article", action = "Details", url = "" });
                endpoints.MapControllerRoute("ArticleEdit", "Clanky/Edit/{url}", defaults: new { controller = "Article", action = "Edit", url = "" });
                endpoints.MapControllerRoute("default", "{controller=Article}/{action=Index}");
             
            });
           /* roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            WebUser user = userManager.FindByEmailAsync("adminEmail").Result;
            userManager.AddToRoleAsync(user, "Admin").Wait();*/
        }
    }
}
