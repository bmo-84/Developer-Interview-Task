using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace InterviewTask.Services
{
    public class LoggingService : ILoggingService
    {
        private string _logFileLocation;
        private LogLevel _systemLogLevel;
        private HttpRequestBase _request;

        public LoggingService(HttpRequestBase request)
        {
            _logFileLocation = System.Configuration.ConfigurationManager.AppSettings["LogFileLocation"].ToString();
            Enum.TryParse(System.Configuration.ConfigurationManager.AppSettings["LogLevel"], out LogLevel retrievedLogLevel);
            _systemLogLevel = retrievedLogLevel;
            _request = request;
        }

        public void LogInformation(string message)
        {
            LogRecord(LogLevel.Information, message);
        }

        public void LogWarning(string message)
        {
            LogRecord(LogLevel.Warning, message);
        }

        public void LogError(string message)
        {
            LogRecord(LogLevel.Error, message);
        }

        private void LogRecord(LogLevel logLevel, string message)
        {
            if (logLevel >= _systemLogLevel)
            {
                string[] logRecord =
                {
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    logLevel.ToString(),
                    _request.UserHostAddress,
                    _request.Path,
                    message
                };

                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + _logFileLocation);

                var fileName = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + _logFileLocation + "\\LogFile_" + DateTime.Today.ToString("yyy-MM-dd") + ".txt";

                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(String.Join("|", logRecord));
                }
            }
        }

        enum LogLevel
        {
            Information,
            Warning,
            Error
        }
    }
}