using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nami.DXP.Common;
using Nami.DXP.Domain;
using Nami.DXP.Persistence;
using System;
using System.IO;

namespace Nami.DXP.IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private DirectoryInfo GetKeyRingDirectoryInfo()
        {
            string applicationBasePath = System.AppContext.BaseDirectory;
            DirectoryInfo directoryInof = new DirectoryInfo(applicationBasePath);
            string keyRingPath = Configuration.GetSection("AppKeys").GetValue<string>("keyRingPath");
            do
            {
                directoryInof = directoryInof.Parent;

                DirectoryInfo keyRingDirectoryInfo = new DirectoryInfo($"{directoryInof.FullName}{keyRingPath}");
                if (keyRingDirectoryInfo.Exists)
                {
                    return keyRingDirectoryInfo;
                }

            }
            while (directoryInof.Parent != null);
            throw new Exception($"key ring path not found.");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection()
            .PersistKeysToFileSystem(GetKeyRingDirectoryInfo())
            .SetApplicationName("SharedCookieApp");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Services to apply Authentication on all controllers
            services.AddControllersWithViews(config =>
            {
                // using Microsoft.AspNetCore.Mvc.Authorization;
                // using Microsoft.AspNetCore.Authorization;
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.Configure<IdentityServerOptions>(Configuration.GetSection(nameof(IdentityServerOptions)));

            //Services for Database & DBConnection
            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultDbConnection"))
            );
            services.AddDbContextPool<NAMI_PLANT_DATAContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("NamiDbConnection"))
           );

            //Services for Authenticated User Identity

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 4;

            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNet.SharedCookie";
                options.AccessDeniedPath = new PathString("/Error/AccessDenied");
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
            });

            //Services to Register Repositories

            services.AddScoped<INamiUserRepository, NamiUserRepository>();
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
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
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
                    pattern: "{controller=Account}/{action=Login}");
            });
        }
    }
}
