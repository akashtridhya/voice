using voice.dapper;
using System;

namespace voice.logs.implementations
{
    public class LogManager : ILogManager
    {
        #region Object and Contructor defination
        private const string CreateTable = @"IF object_id('[dbo].[ApplicationLogs]') IS NULL BEGIN CREATE TABLE [dbo].[ApplicationLogs]([Id] [uniqueidentifier] NOT NULL,[Category] [nvarchar](50) NULL,[Message] [nvarchar](max) NULL,[Details] [nvarchar](max) NULL,[Created] [datetime2](7) NULL,CONSTRAINT [PK_ApplicationLogs]PRIMARY KEY CLUSTERED  ( [Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ALTER TABLE [dbo].[ApplicationLogs] ADD  CONSTRAINT [DF_ApplicationLogs_Id]  DEFAULT (newid()) FOR [Id]; ALTER TABLE [dbo].[ApplicationLogs] ADD  CONSTRAINT [DF_ApplicationLogs_Created]  DEFAULT (getdate()) FOR [Created] END";
        private IDapperRepository DapperRepository { get; set; }

        public LogManager(IDapperRepository DapperRepository)
        {
            this.DapperRepository = DapperRepository;
            DapperRepository.AddOrUpdate(CreateTable, new { }, System.Data.CommandType.Text);
        }

        public void AddOrUpdateLogs(string category, string message, string details = null)
        {
           var response =  DapperRepository.AddOrUpdate<dynamic>("INSERT INTO ApplicationLogs (Details, Message, Category) VALUES (@Details, @Message, @Category)", new ApplicationLogs { Details = details, Message = message, Category = category }, System.Data.CommandType.Text);
        }
        #endregion

        public void LogInfo(string message, string details = null)
        {
            AddOrUpdateLogs("info", message, details);
        }

        public void LogError(string message, Exception details = null)
        {
            AddOrUpdateLogs("error", message, details.StackTrace.ToString());
        }

        public void LogWarning(string message, string details = null)
        {
            AddOrUpdateLogs("warning", message, details);
        }

        public void LogDebug(string message, string details = null)
        {
            AddOrUpdateLogs("debug", message, details);
        }

        public void LogCritical(string message, string details = null)
        {
            AddOrUpdateLogs("critical", message, details);
        }

        public void LogTrace(string message, string details = null)
        {
            AddOrUpdateLogs("trace", message, details);
        }

        #region Dispose
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    #region Model defination
    class ApplicationLogs
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Category { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
    #endregion
}