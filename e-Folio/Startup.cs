using eFolio.DTO;
using eFolio.BL;
using eFolio.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using eFolio.API;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddDbContext<eFolioDBContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IDeveloperService, DeveloperService>();

            //Automapping
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new Automapper()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "eFolio",
                    Description = "eFolio ASP.NET Core Web API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "eFolio Test API V1");
            });
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<eFolioDBContext>();
                context.Database.Migrate();
                ContextInitializer.Initialize(context);
            }
            app.UseMvc();
        }
    }
}
