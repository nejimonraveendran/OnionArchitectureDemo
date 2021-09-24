using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.CrossCutting.Logging.Implementations
{
    public sealed class NLogSqlOptions
    {
        public string InstallConnectionString { get; set; }
        public string ConnectionString { get; set; }
        public string LogsTableName { get; set; }

        public bool CreateDatabaseIfNotExists { get; set; }
        public bool CreateLogsTableIfNotExists { get; set; }
    }
}
