using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace LogLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class NLog:ILoggingService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Log to text file
        /// </summary>
        /// <param name="message">
        /// message to print to screen
        /// </param>
        public void Log(string message)
        {
            logger.Info(message);
        }



    }
}
