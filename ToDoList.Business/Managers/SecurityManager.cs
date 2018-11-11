using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Entities;
using ToDoList.Common.Exceptions.Security;
using ToDoList.Common.Managers.Log;
using ToDoList.Common.Managers;
using ToDoList.Data;

namespace ToDoList.Business.Managers
{
    public class SecurityManager : ISecurityManager
    {
        private static ILogger _log;
        //private static User _loggedUser;
        ISessionContextManager _sessionContextManager;
        ToDoListDbContext _db = new ToDoListDbContext();

        public SecurityManager(ILogManager log, ISessionContextManager sessionContextManager)
        {
            _log = log.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            _sessionContextManager = sessionContextManager;
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _log.Debug("Release resources");
                _db.Dispose();
            }
        }

        public User GetLoggedUser()
        {
            _log.Debug("Check loggedUser is null");
            if (_sessionContextManager.GetSessionToken() == null)
            {
                _log.Error("SessionToken not found");
                throw new SecurityException("SessionToken not found");
            }
            if ((User)_sessionContextManager.GetValue("User") == null)
            {
                _log.Error("No user currently logged in");
                throw new SecurityException("No user currently logged in");
            }
            _log.Debug("Return loggedUser");

            return (User)_sessionContextManager.GetValue("User");
        }

        public bool IsUserLogged()
        {
            return (User)_sessionContextManager.GetValue("User") != null;
        }

        public string Login(string username, string password)
        {
            
            _log.Debug("Check user exist in db");
            var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            _log.Debug("Check user is null");
            if (user == null)
            {
                _log.Error("Invalid username or password");
                throw new LoginFailedException("Invalid username or password");
            }
            _log.Debug("Check username or password are empty ");
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                _log.Error("Invalid username or password");
                throw new LoginFailedException("Invalid username or password");
            }
             
            string token = Guid.NewGuid().ToString();


            _sessionContextManager.SetSessionToken(token);
            _sessionContextManager.SetValue("User", user);

            _db.Entry<User>(user).State = System.Data.Entity.EntityState.Detached;
            _log.Debug("Value loggedUser");
            //_loggedUser = user;
            //_loggedUser = (User)_sessionContextManager.GetValue("User");
            _log.Info("User is logged");

            return _sessionContextManager.GetSessionToken();
        }

        public void Logout()
        {
            if (!IsUserLogged())
            {
                _log.Error("No user currently logged in");
                throw new SecurityException("No user currently logged in");
            }

            _log.Debug("Value loggedUser is null");
            //_loggedUser = null;
            _sessionContextManager.RemoveValue("User");
            _sessionContextManager.RemoveSessionToken();
            _log.Info("User is logout");
        }
    }
}
 