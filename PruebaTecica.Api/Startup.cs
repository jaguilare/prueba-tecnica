using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PruebaTecnica.Application.Services;
using PruebaTecnica.Core.Interfaces.IRepositories;
using PruebaTecnica.Core.Interfaces.Services;
using PruebaTecnica.Infrastructure;
using PruebaTecnica.Infrastructure.Repositories;

namespace PruebaTecica.Api
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

            services.AddControllers();

            // Contexto connection DB
            services.AddDbContext<PruebaTecnicaContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Connection"));
            });

            // Injection Services and Repositories
            services.AddTransient<IClientesService, ClientesService>();
            services.AddTransient<IClientesRepository, ClientesRepository>();

            services.AddTransient<IPersonasRepository, PersonasRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
