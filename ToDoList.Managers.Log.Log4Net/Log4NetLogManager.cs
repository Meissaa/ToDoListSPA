using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ToDoList.Managers.Log.Log4Net
{
    /// <summary>
    /// ILogManager implementation based on Log4Net library
    /// </summary>
    public class Log4NetLogManager : ToDoList.Common.Managers.Log.ILogManager
    {
        #region Constructors

        static Log4NetLogManager()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Create an instance of Log4NetLogManager class.
        /// The default logger name is matched by the DeclaringType property of the calling methods
        /// </summary>
        public Log4NetLogManager()
        {
        }

        #endregion

        #region ILogManager Implementation

        public ToDoList.Common.Managers.Log.ILogger GetLogger()
        {
            return GetLogger("root");
        }

        public ToDoList.Common.Managers.Log.ILogger GetLogger(string loggerName)
        {
            return new Log4NetLogger(LogManager.GetLogger(loggerName));
        }

        #endregion


    }
}
