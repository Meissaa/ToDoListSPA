using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Entities;

namespace ToDoList.Common.Managers
{
    public interface ISecurityManager : IDisposable
    {
        string Login(string username, string password);
        User GetLoggedUser();
        bool IsUserLogged();
        void Logout();

    }
}
