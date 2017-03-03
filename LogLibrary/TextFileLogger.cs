using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogLibrary
{
    /// <summary>
    /// Logger to implement ILoggingService
    /// </summary>
    public class    TextFileLogger : ILoggingService
    {
        /// <summary>
        /// Writes a message to a text file
        /// </summary>
        /// <param name="message"> 
        /// The message to write to a text file
        /// </param>
        public void Log(string message)
        {
            string filePath = "log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(message);
                }

            }
            else
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(message);
                }

            }

        }






    }
}
