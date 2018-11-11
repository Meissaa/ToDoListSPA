using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Common.Managers
{
    public interface ISessionContextManager
    {
        void SetSessionToken(string sessionToken);
        string GetSessionToken();
        void RemoveSessionToken();
        object GetValue(string key);
        void SetValue(string key, object data);
        void RemoveValue(string key);
    }
}
