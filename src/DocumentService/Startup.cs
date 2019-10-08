using DocumentServices.Common.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DocumentService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly MicroserviceBootstrapper _microserviceBootstrapper;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _microserviceBootstrapper = GetMicroserviceBootstrapper();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _microserviceBootstrapper.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _microserviceBootstrapper.Configure(app, env, loggerFactory);
        }

        #region Private

        private MicroserviceBootstrapper GetMicroserviceBootstrapper()
        {
            var microserviceConfiguration = new MicroserviceConfiguration
            {
                ServiceName = _configuration["ServiceName"],
                AppSettings = _configuration,
                AssembliesToScan = new[] { this.GetType().Assembly },
                SwaggerConfiguration = new SwaggerConfiguration
                {
                    Title = _configuration["SwaggerConfiguration:Title"],
                    Version = _configuration["SwaggerConfiguration:Version"],
                    Description = _configuration["SwaggerConfiguration:Description"]
                }
            };

            return new MicroserviceBootstrapper(microserviceConfiguration);
        }

        #endregion
    }
}
