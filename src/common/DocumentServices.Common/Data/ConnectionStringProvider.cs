using Core.Providers;
using DocumentServices.Common.Exceptions;
using Microsoft.Extensions.Configuration;

namespace DocumentServices.Common.Data
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get(string connectionName)
        {
            string connectionString = _configuration.GetConnectionString(connectionName);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new MicroserviceException("Connection String cannot be null or empty");
            }

            return connectionString;
        }

        public string Get()
        {
            return Get("DefaultConnection");
        }
    }
}
