using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Options
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; } = String.Empty;
        public int MaxRetryCount { get; set; } = 3;
        public int CommandTimeout { get; set; } = 30;
        public bool EnableDetailedErrors { get; set; } = true;
        public bool EnableSensitiveDataLogging { get; set; } = true;

    }
}
