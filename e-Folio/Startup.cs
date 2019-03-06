
using System;
using eFolio.DTO;
using eFolio.API;
using eFolio.BL;
using eFolio.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using eFolio.Api.Seeds;
using eFolio.API.Models;
using eFolio.API.Seeds;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

namespace e_Folio
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
            string connection = Configuration.GetConnectionString("EFolioConnection");

            services.AddDbContext<eFolioDBContext>(options => 
                options.UseSqlServer(connection));

            services.AddDbContext<AuthDBContext>(options =>
                options.UseSqlServer(connection));

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IDeveloperService, DeveloperService>();

            //Automapping
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new Automapper()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // For auth
            //services.AddTransient<IEmailSender, EmailSender>();
            services.AddIdentity<UserEntity, IdentityRole<int>>(conf =>
                {
                    conf.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<AuthDBContext>()
                .AddDefaultTokenProviders();

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(AuthConfig.GetIdentityResources())
                .AddInMemoryApiResources(AuthConfig.GetApiResources())
                .AddInMemoryClients(AuthConfig.GetClients())
                .AddAspNetIdentity<UserEntity>();

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddMvc(); //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000/";

                    options.RequireHttpsMetadata = false;

                    options.ApiName = "e-FolioAPI";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                try
                {
                    var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                    var context = scope.ServiceProvider.GetService<eFolioDBContext>();

                    var userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
                    var contextForAuth = serviceProvider.GetRequiredService<AuthDBContext>();

                    context.Database.Migrate();
                    contextForAuth.Database.Migrate();

                    ContextInitializer.Initialize(context);
                    ContextInitializerForAuth.Initialize(contextForAuth, userManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex,"An error occured while seeding the database.");
                }
                //using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                //{
                //    var context = scope.ServiceProvider.GetService<eFolioDBContext>();
                //    context.Database.Migrate();
                //    ContextInitializer.Initialize(context);
                //}
            }

            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
