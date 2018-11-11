using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Entities;
using ToDoList.Common.Managers;
using ToDoList.Common.Managers.Log;
using ToDoList.Common.Exceptions.Security;
using ToDoList.Data;


namespace ToDoList.Business.Managers
{
    public class UserManager : IUserManager
    {
        private static ILogger _log; 
        ToDoListDbContext _db = new ToDoListDbContext();
        
        public UserManager(ILogManager log)
        {
            _log = log.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
        }

        public void Create(User user)
        {
            _log.Debug("Check user is null");
            if (user == null )
            {
                _log.Error("Account not created.");
                throw new CreateUserFailedException("Account not created.");
            }
            _log.Debug("Check username or password or firstname or lastname or emailaddress are empty ");
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.FirstName) 
                || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.EmailAddress)) {
                _log.Error("Field is empty");
                throw new CreateUserFailedException("Fill in all fields.");
            }

            _log.Debug("Add user to db");
            _db.Users.Add(user);
            _db.SaveChanges();
            _log.Info("User added to db");
        }

       public void Dispose()
        {
            if (_db != null)
            {
                _log.Debug("Release resources");
                _db.Dispose();
            }
        }
    
    }
}
