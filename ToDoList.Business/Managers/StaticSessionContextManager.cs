using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Managers;
using ToDoList.Common.Managers.Log;

namespace ToDoList.Business.Managers
{
    public class StaticSessionContextManager : ISessionContextManager
    {
        private ILogger _log;

        [ThreadStatic]
        static ConcurrentDictionary<string, object> _sessionObject = new ConcurrentDictionary<string, object>();

        public StaticSessionContextManager(ILogManager logManager)
        {
            if (logManager == null)
            {
                throw new ArgumentNullException("logManager");
            }

            _log = logManager.GetLogger(typeof(StaticSessionContextManager).FullName);
        }

        public object GetValue(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            _log.DebugFormat("key:{0}", key);
            return (object)_sessionObject[key];
        }

        public void RemoveValue(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            _log.DebugFormat("key:{0}", key);
            _sessionObject[key] = null;
        }

        public void SetValue(string key, object data)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            _log.DebugFormat("key:{0}", key);
            _sessionObject[key] = data;
        }

        public void SetSessionToken(string sessionToken)
        {
        }

        public string GetSessionToken()
        {
            return null;
        }

        public void RemoveSessionToken()
        {
        }

    }
}
