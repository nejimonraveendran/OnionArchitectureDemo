using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using OnionApp.CrossCutting.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace OnionApp.CrossCutting.Logging.Implementations
{
    public class NLogWebSqlLogger : IWebLogger
    {
        Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public NLogWebSqlLogger()
        {
            configureDbLogger("Server=.;Database=LogsDb2;Integrated Security=true;");
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


        private void configureDbLogger(string strConnectionString)
        {
            var sb = new StringBuilder();
            var installationContext = new InstallationContext();

            var builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = strConnectionString;
            string strDatabase = builder.InitialCatalog;

            var targetDB = new NLog.Targets.DatabaseTarget();

            targetDB.Name = "logsDb";
            targetDB.ConnectionString = strConnectionString;

            NLog.Targets.DatabaseParameterInfo dbParam;

            dbParam = new NLog.Targets.DatabaseParameterInfo();
            dbParam.Name = string.Format("@Message");
            dbParam.Layout = string.Format("${{message}}");
            targetDB.Parameters.Add(dbParam);
            targetDB.CommandText = string.Format("INSERT INTO Logs(Message) VALUES (@message);");

            // Keep original configuration
            LoggingConfiguration config = LogManager.Configuration;
            if (config == null)
                config = new LoggingConfiguration();

            config.AddTarget(targetDB.Name, targetDB);

            var rule = new LoggingRule("*", LogLevel.Debug, targetDB);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            var builder2 = new SqlConnectionStringBuilder();
            builder2.ConnectionString = strConnectionString;
            builder2.InitialCatalog = "master";

            // we have to connect to master in order to do the install because the DB may not exist
            targetDB.InstallConnectionString = builder2.ConnectionString;

            sb.AppendLine(string.Format("IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'{0}')", strDatabase));
            sb.AppendLine(string.Format("CREATE DATABASE {0}", strDatabase));

            var createDBCommand = new DatabaseCommandInfo();
            createDBCommand.Text = sb.ToString();
            createDBCommand.CommandType = System.Data.CommandType.Text;
            targetDB.InstallDdlCommands.Add(createDBCommand);

            // create the database if it does not exist

            targetDB.Install(installationContext);

            targetDB.InstallDdlCommands.Clear();
            sb.Clear();
            sb.AppendLine("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Logs')");
            sb.AppendLine("RETURN");
            sb.AppendLine("");
            sb.AppendLine("CREATE TABLE [dbo].[Logs](");
            sb.AppendLine("[LogId] [int] IDENTITY(1,1) NOT NULL,");
            sb.AppendLine("[Message] [nvarchar](max) NULL,");
            sb.AppendLine(" CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ");
            sb.AppendLine("(");
            sb.AppendLine("[LogId] ASC");
            sb.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
            sb.AppendLine(") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

            DatabaseCommandInfo createTableCommand = new DatabaseCommandInfo();
            createTableCommand.Text = sb.ToString();
            createTableCommand.CommandType = System.Data.CommandType.Text;
            targetDB.InstallDdlCommands.Add(createTableCommand);

            // we can now connect to the target DB
            targetDB.InstallConnectionString = strConnectionString;

            // create the table if it does not exist
            targetDB.Install(installationContext);
        }
    }
}
