
using eFolio.DTO;
using e_Folio.Seeds;
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

          //  services.AddScoped<IRepository<Project>, ProjectRepository>();
         
            services.AddSingleton<IRepository<DeveloperEntity>>(
                serviceCollection => new DeveloperRepository(connection)
            );


            //Automapping
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new Automapper()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<eFolioDBContext>();
                context.Database.Migrate();
                ContextInitializer.Initialize(context);
            }
            app.UseMvc( );
        }
    }
}
