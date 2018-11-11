using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Entities;

namespace ToDoList.Common.Managers
{
    public interface IUserManager: IDisposable
    {
        void Create(User user);
    }
}
