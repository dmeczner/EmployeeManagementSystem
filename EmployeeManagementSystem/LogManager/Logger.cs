using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace EmployeeManagementSystem.LogManager
{
    public class LogProps
    {
        public string DateTime { get; set; }
        public string LogLevel { get; set; } = "N/A";
        public string FileName { get; set; } = "";
        public string MemberName { get; set; } = "";
        public int SourLineNumber { get; set; } = 0;
        public string MessageName { get; set; }
    }

    public static class Logger
    {
        public static void Information(string message,
                                        [CallerMemberName] string memberName = "",
                                        [CallerFilePath] string sourceFilePath = "",
                                        [CallerLineNumber] int sourceLineNumber = 0)
        {
            LogProps logProps = new LogProps
            {
                MessageName = message,
                MemberName = memberName,
                FileName = sourceFilePath,
                SourLineNumber = sourceLineNumber
            };
            SendLog(logProps).Information(message);
        }

        public static void Debug(string message,
                                        [CallerMemberName] string memberName = "",
                                        [CallerFilePath] string sourceFilePath = "",
                                        [CallerLineNumber] int sourceLineNumber = 0)
        {
            LogProps logProps = new LogProps
            {
                MessageName = message,
                MemberName = memberName,
                FileName = sourceFilePath,
                SourLineNumber = sourceLineNumber
            };
            SendLog(logProps).Debug(message);
        }

        public static void Error(Exception ex,
                                 [CallerMemberName] string memberName = "",
                                 [CallerFilePath] string sourceFilePath = "",
                                 [CallerLineNumber] int sourceLineNumber = 0)
        {
            LogProps logProps = new LogProps
            {
                MessageName = ex != null ? ex.ToString() : "",
                MemberName = memberName,
                FileName = sourceFilePath,
                SourLineNumber = sourceLineNumber
            };

            SendLog(logProps).Error(ex != null ? ex.ToString() : "");
        }

        public static void Error(string message,
                                        Exception ex,
                                        [CallerMemberName] string memberName = "",
                                        [CallerFilePath] string sourceFilePath = "",
                                        [CallerLineNumber] int sourceLineNumber = 0)
        {
            LogProps logProps = new LogProps
            {
                MessageName = FormatForException(message, ex),
                MemberName = memberName,
                FileName = sourceFilePath,
                SourLineNumber = sourceLineNumber
            };
            SendLog(logProps).Error(FormatForException(message, ex));
        }

        private static Serilog.ILogger SendLog(LogProps logProps, [CallerMemberName] string level = "")
        {
            var fileName = Path.GetFileNameWithoutExtension(logProps.FileName);
            string logLevelAbbr;
            switch (level)
            {
                case nameof(LogLevel.Information):
                    logLevelAbbr = "INF";
                    break;
                case nameof(LogLevel.Debug):
                    logLevelAbbr = "DBG";
                    break;
                case nameof(LogLevel.Warning):
                    logLevelAbbr = "WRN";
                    break;
                case nameof(LogLevel.Error):
                    logLevelAbbr = "ERR";
                    break;
                default:
                    logLevelAbbr = "N/A";
                    break;
            }
            logProps.LogLevel = logLevelAbbr;

            return Serilog.Log.ForContext("Log_Level", logLevelAbbr)
                              .ForContext("File_Name", fileName)
                              .ForContext("Method_Name", logProps.MemberName)
                              .ForContext("Line", logProps.SourLineNumber);
        }

        private static string FormatForException(string message, Exception ex)
        {
            return $"{message} : {(ex != null ? "Exeption Message " + ex.Message + "StackTrace: " + ex.StackTrace : "")}";
        }
    }
}
