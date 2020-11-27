using System;
using System.IO;
using System.Text;
using XeroProducts.Data.Models;

namespace XeroProducts.Data.Utils
{
    /// <summary>
    /// Utils class <c>Helper</c>
    /// </summary>
    public static class Helper
    {

        /// <summary>
        /// Method to return the object of exception log <c>ErrorLog</c>.
        /// </summary>
        /// <param name="ex">The object of Exception.</param>
        /// <returns></returns>
        public static ErrorDetails GetErrorLogObject(Exception ex)
        {
            var exceptionLog = new ErrorDetails()
            {
                Id = new Guid(),               
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                ExceptionDate = DateTime.Now
            };

            return exceptionLog;
        }

        /// <summary>
        /// Method to log the exception in a file.
        /// </summary>
        /// <param name="ex" cref="Exception" >Exception object</param>
        public static void LogErrorInFile(Exception ex)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"********************************* Exception - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} *********************************");
            builder.AppendLine();
            builder.AppendLine($"Exception: { ex.Message}");
            builder.AppendLine($"Stacktrace: { ex.StackTrace }");
            builder.AppendLine();
            builder.AppendLine($"******************************************************************************************************************************");

            string fileName = $"XeroProductErrorLog_{DateTime.Now.ToString("yyyyMMdd")}.txt";

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + fileName,
                  builder.ToString());

            throw new Exception($"The exception has been logged into the file:  {fileName}");
        }

    }
}
