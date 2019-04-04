using AutoMapper;
using eFolio.API.Seeds;
using eFolio.BL;
using eFolio.EF;
using eFolio.Elastic;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Security.Principal;

namespace eFolio.API
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

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "eFolio",
                    Description = "eFolio ASP.NET Core Web API"
                });
            });

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

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000/";

                    options.RequireHttpsMetadata = false;

                    options.ApiName = "e-FolioAPI";
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            // app.UseCors(options => options.WithOrigins("http://localhost:5000/").AllowAnyMethod().AllowAnyHeader());
            app.UseCors("AllowAllHeaders");
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

                    ContextInitializer.Initialize(context, new ElasticSearch());
                    ContextInitializerForAuth.Initialize(contextForAuth, userManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while seeding the database.");
                } 
            }

            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
