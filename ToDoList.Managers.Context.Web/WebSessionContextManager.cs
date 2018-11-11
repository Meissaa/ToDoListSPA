using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Managers;
using System.Web;


namespace ToDoList.Managers.Context.Web
{
    public class WebSessionContextManager : ISessionContextManager
    {
        public string GetSessionToken()
        {
            return null;
        }

        public object GetValue(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            CheckContext();
            return (object)HttpContext.Current.Session[key];
        }

        public void RemoveSessionToken()
        {
        }

        public void RemoveValue(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            CheckContext();
            HttpContext.Current.Session[key] = null;
        }

        public void SetSessionToken(string sessionToken)
        {
        }

        public void SetValue(string key, object data)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            CheckContext();
            HttpContext.Current.Session[key] = data;
        }

        private void CheckContext()
        {
            if (HttpContext.Current == null)
                throw new ApplicationException("HttpContext.Current is null");

            if (HttpContext.Current.Session == null)
                throw new ApplicationException("HttpContext.Current.Session is null");
        }
    }
}
