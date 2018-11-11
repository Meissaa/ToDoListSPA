using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Managers.Log.Log4Net
{


    public class Log4NetLogger : ToDoList.Common.Managers.Log.ILogger
    {

        #region Constructors

        public Log4NetLogger(ILog log)
        {
            _log = log;
        }


        #endregion

        #region Private Members

        #region Private Fields

        ILog _log;

        #endregion

        #region Private Methods

        private void Log(Level level, object message, Exception t)
        {
            try
            {
                if (_log.Logger.IsEnabledFor(level))
                    _log.Logger.Log(new StackFrame(1).GetMethod().DeclaringType, level, message, t);
            }
            catch (Exception)
            {

            }
        }

        private void LogFormat(Level level, IFormatProvider formatProvider, string format, params object[] args)
        {
            try
            {
                if (_log.Logger.IsEnabledFor(level))
                    _log.Logger.Log(
                        new StackFrame(1).GetMethod().DeclaringType,
                        level,
                        String.Format(formatProvider, format, args),
                        null);
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #endregion

        #region ILogger Implementation

        public bool IsDebugEnabled
        {
            get
            {
                try
                {
                    return _log.IsDebugEnabled;
                }
                catch (Exception)
                {
                    //no way to log anything
                    return false;
                }
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                try
                {
                    return _log.IsInfoEnabled;
                }
                catch (Exception)
                {
                    //no way to log anything
                    return false;
                }
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                try
                {
                    return _log.IsWarnEnabled;
                }
                catch (Exception)
                {
                    //no way to log anything
                    return false;
                }
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                try
                {
                    return _log.IsErrorEnabled;
                }
                catch (Exception)
                {
                    //no way to log anything
                    return false;
                }
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                try
                {
                    return _log.IsFatalEnabled;
                }
                catch (Exception)
                {
                    //no way to log anything
                    return false;
                }
            }
        }

        public void Debug(object message)
        {
            Log(Level.Debug, message, null);
        }

        public void Info(object message)
        {
            Log(Level.Info, message, null);
        }

        public void Warn(object message)
        {
            Log(Level.Warn, message, null);
        }

        public void Error(object message)
        {
            Log(Level.Error, message, null);
        }

        public void Fatal(object message)
        {
            Log(Level.Fatal, message, null);
        }

        public void Debug(object message, Exception t)
        {
            Log(Level.Debug, message, t);
        }

        public void Info(object message, Exception t)
        {
            Log(Level.Info, message, t);
        }

        public void Warn(object message, Exception t)
        {
            Log(Level.Warn, message, t);
        }

        public void Error(object message, Exception t)
        {
            Log(Level.Error, message, t);
        }

        public void Fatal(object message, Exception t)
        {
            Log(Level.Fatal, message, t);
        }

        public void DebugFormat(string format, params object[] args)
        {
            LogFormat(Level.Debug, null, format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            LogFormat(Level.Info, null, format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            LogFormat(Level.Warn, null, format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            LogFormat(Level.Error, null, format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            LogFormat(Level.Fatal, null, format, args);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogFormat(Level.Debug, provider, format, args);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogFormat(Level.Info, provider, format, args);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogFormat(Level.Warn, provider, format, args);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogFormat(Level.Error, provider, format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogFormat(Level.Fatal, provider, format, args);
        }

        #endregion

    }
}
