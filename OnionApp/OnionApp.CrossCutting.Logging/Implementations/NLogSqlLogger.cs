using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NLog.Web;
using OnionApp.CrossCutting.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace OnionApp.CrossCutting.Logging.Implementations
{
    public class NLogSqlLogger : IAppLogger
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        public NLogSqlLogger(NLogSqlOptions options)
        {
            configureDbLogger(options);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }


        //created based on the answer here
        //https://stackoverflow.com/questions/20101809/creating-a-database-programatically-in-nlog-to-enable-using-databasetarget
        private void configureDbLogger(NLogSqlOptions nLogSqlOptions)
        {
            var installationContext = new InstallationContext();

            var cnStringBuilder = new SqlConnectionStringBuilder();
            cnStringBuilder.ConnectionString = nLogSqlOptions.ConnectionString;
            var logsDbName = cnStringBuilder.InitialCatalog;

            //set up NLog Target (DB)
            var targetDb = new DatabaseTarget
            {
                Name = "logsDb",
                ConnectionString = cnStringBuilder.ConnectionString
            };

            //set up NLog insert query and parameters (Log DB columns)
            targetDb.CommandText = string.Format($"INSERT INTO {nLogSqlOptions.LogsTableName}(LogLevel, LogDate, Message) VALUES (@LogLevel, @LogDate, @Message);");
            targetDb.Parameters.Add(new DatabaseParameterInfo { Name = "@LogLevel", Layout = "${level}" });
            targetDb.Parameters.Add(new DatabaseParameterInfo { Name = "@LogDate", Layout = "${date}" });
            targetDb.Parameters.Add(new DatabaseParameterInfo { Name = "@Message", Layout = "${message}" });


            //Keep original configuration
            LoggingConfiguration config = LogManager.Configuration;
            if (config == null)
                config = new LoggingConfiguration();

            config.AddTarget(targetDb.Name, targetDb);

            var rule = new LoggingRule("*", LogLevel.Debug, targetDb);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            if(nLogSqlOptions.CreateDatabaseIfNotExists)
                createDbIfNotExists(nLogSqlOptions.InstallConnectionString, installationContext, logsDbName, targetDb);

            if(nLogSqlOptions.CreateLogsTableIfNotExists)
                createTableIfNotExists(nLogSqlOptions.ConnectionString, installationContext, targetDb, nLogSqlOptions.LogsTableName);
        }

        private static void createTableIfNotExists(string connectionString, 
            InstallationContext installationContext, 
            DatabaseTarget targetDb, 
            string logsTable)
        {
            targetDb.InstallDdlCommands.Clear();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = '{logsTable}')");
            stringBuilder.AppendLine("RETURN");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("CREATE TABLE [dbo].[Logs](");
            stringBuilder.AppendLine("[LogId] [int] IDENTITY(1,1) NOT NULL,");
            stringBuilder.AppendLine("[LogLevel] [nvarchar](25) NULL,");
            stringBuilder.AppendLine("[LogDate] [datetime2](7) NULL,");
            stringBuilder.AppendLine("[Message] [nvarchar](max) NULL,");
            stringBuilder.AppendLine(" CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ");
            stringBuilder.AppendLine("(");
            stringBuilder.AppendLine("[LogId] ASC");
            stringBuilder.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
            stringBuilder.AppendLine(") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

            DatabaseCommandInfo createTableCommand = new DatabaseCommandInfo();
            createTableCommand.Text = stringBuilder.ToString();
            createTableCommand.CommandType = System.Data.CommandType.Text;
            targetDb.InstallDdlCommands.Add(createTableCommand);

            // we can now connect to the target DB
            targetDb.InstallConnectionString = connectionString;

            // create the table if it does not exist
            targetDb.Install(installationContext);
        }

        private void createDbIfNotExists(string installConnectionString, 
            InstallationContext installationContext, 
            string logsDbName, 
            DatabaseTarget targetDb)
        {
            var installCnStringBuilder = new SqlConnectionStringBuilder();
            installCnStringBuilder.ConnectionString = installConnectionString;
            installCnStringBuilder.InitialCatalog = "master";

            // we have to connect to master in order to do the install because the DB may not exist
            targetDb.InstallConnectionString = installCnStringBuilder.ConnectionString;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'{logsDbName}')");
            stringBuilder.AppendLine($"CREATE DATABASE {logsDbName}");

            var createDbCommand = new DatabaseCommandInfo();
            createDbCommand.Text = stringBuilder.ToString();
            createDbCommand.CommandType = System.Data.CommandType.Text;
            targetDb.InstallDdlCommands.Add(createDbCommand);

            // create the database if it does not exist
            targetDb.Install(installationContext);
            
        }
    }
}
