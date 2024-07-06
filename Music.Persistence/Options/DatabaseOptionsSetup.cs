using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Music.Persistence.Options
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private const string ConfigerationSectionName = "DatabaseOptions";

        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(DatabaseOptions options)
        {
            var connectionStrings = _configuration.GetConnectionString("DefaultConnection");
            options.ConnectionString = connectionStrings ?? String.Empty;

            _configuration.GetSection(ConfigerationSectionName).Bind(options);

        }
    }
}
