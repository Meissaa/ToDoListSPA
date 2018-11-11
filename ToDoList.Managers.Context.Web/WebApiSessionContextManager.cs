using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Managers;
using ToDoList.Common.Managers.Log;

namespace ToDoList.Managers.Context.Web
{
    public class WebApiSessionContextManager : ISessionContextManager
    {
        string _sessionToken;
        static ConcurrentDictionary<string, object> _sessionObject = new ConcurrentDictionary<string, object>();

        public void SetSessionToken(string sessionToken)
        {
            _sessionToken = sessionToken;
        }

        public string GetSessionToken()
        {
            return _sessionToken;
        }

        public void RemoveSessionToken()
        {
            _sessionToken = null;
        }

        public object GetValue(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            var fullKey = String.Format("{0}___{1}", _sessionToken, key);

            if (!_sessionObject.Keys.Any(g => g == fullKey))
                return null;

            return _sessionObject[fullKey];
        }

        public void RemoveValue(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            object ret;
            _sessionObject.TryRemove(String.Format("{0}___{1}", _sessionToken, key), out ret);
        }

        public void SetValue(string key, object data)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            _sessionObject[String.Format("{0}___{1}", _sessionToken, key)] = data;
        }
    }
}
