using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using FluentValidation.AspNetCore;
using Graduates.Configurations.CustomConfigurations;
using Graduates.Core.Entities;
using Graduates.Data;
using Graduates.UI.Web.CustomConfigurations;
using Graduates.Configurations.CustomConfigurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.DataProtection;

namespace Graduates.UI.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment env, IConfiguration config,
            ILoggerFactory loggerFactory)
        {
            _env = env;
            _config = config;
            _loggerFactory = loggerFactory;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // All user accounts on the machine can decrypt the keys
            //services.AddDataProtection()
            //    .ProtectKeysWithDpapi(protectToLocalMachine: true);

            //Automapper Configuration
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<GraduatesContext>(options =>
            {
                options.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
            //.AddRoleManager<RoleManager<ApplicationRole>>()
            .AddEntityFrameworkStores<GraduatesContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie( options =>
                {
                    options.LoginPath = "auth/login";
                    options.AccessDeniedPath = "auth/accessdenied";
                });

            //Application Services and Repositories
            RepositoryConfiguration.Configure(services);
            ServiceConfiguration.Configure(services);

            services.AddSingleton(this._config);

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddFluentValidation(fvc =>
                fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.Configure<IdentityOptions>(options =>
            { // Password settings 
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings 
                options.User.RequireUniqueEmail = true;
            });

            // DataTables.AspNet registration with default options.
            services.RegisterDataTables();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
                            , IServiceProvider serviceProvider, GraduatesContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            var contentRoot = _env.ContentRootPath;


            // Render only .js files in "Views" folder
            //app.UseStaticFiles(new StaticFileOptions()
            //{

            //    //FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Views")),
            //    FileProvider = new PhysicalFileProvider(Path.Combine(contentRoot, @"Views")),
            //    RequestPath = new PathString("/Views"),
            //    ContentTypeProvider = new FileExtensionContentTypeProvider(
            //            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            //            {
            //                { ".js", "application/javascript" },
            //            })
            //});

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=AdminHome}/{action=Index}/{id?}");
            });

            DbInitializer.InitializeAsync(context, serviceProvider).Wait();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
