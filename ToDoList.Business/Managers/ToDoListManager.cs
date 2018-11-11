using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Entities;
using ToDoList.Common.Exceptions;
using ToDoList.Common.Managers.Log;
using ToDoList.Common.Managers;
using ToDoList.Data;

namespace ToDoList.Business.Managers
{
    public class ToDoListManager : IToDoListManager
    {
        private static ILogger _log; 
        ToDoListDbContext _db = new ToDoListDbContext();
        ToDoListItem _toDoListItem;
        ISecurityManager _securityManager;

        public ToDoListManager(ILogManager log, ISecurityManager securityManager)
        {
            _log = log.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            _securityManager = securityManager;
        }


        public void AddTaskToList(int listItemId, ToDoListTask task)
        {
            _log.Debug("Check task is null");
            if (task == null)
            {
                _log.Error("Task not added");
                throw new ArgumentNullException("Task not added");
            }
            _log.Debug("Check value taskText is empty");
            if (string.IsNullOrEmpty(task.Text))
            {
                _log.Error("Field Text is empty");
                throw new ToDoListException("Field Text is empty");
            }

            _log.Debug("Check list exist in db");
            var list = _db.ToDoListItems.FirstOrDefault(l => l.Id == listItemId);

            _log.Debug("Check list is null");
            if (list == null)
            {
                _log.ErrorFormat("List with id {0} not found", listItemId);
                throw new ToDoListException(String.Format("List with id {0} not found", listItemId));
            }

            task.ToDoListItem = list;
            _log.Debug("Add task to list Tasks");
            list.Tasks.Add(task);
            _log.Debug("Add task to db");
            _db.SaveChanges();
            _log.Info("Task added to db");
        }

        public void CreateList(ToDoListItem item)
        {
            _log.Debug("Check list is null");
            if (item == null)
            {
                _log.Error("List not created");
                throw new ArgumentNullException("List not created");
            }
            _log.Debug("Check value listname is empty");
            if (string.IsNullOrEmpty(item.Name))
            {
                _log.Error("List name is empty");
                throw new ToDoListException("List name is empty");
            }
            _log.Debug("Value userId");
            var userId = _securityManager.GetLoggedUser().Id;
            item.User = _db.Users.First(u => u.Id == userId);
            _log.Debug("Add list to db");
            _db.ToDoListItems.Add(item);
            _db.SaveChanges();
            _log.Debug("Added list to db");
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _log.Debug("Release resources");
                _db.Dispose();
            }
        }

        public ToDoListItem GetListItemDetail(int listItemId)
        {
            _log.Debug("Value loggedUser");
            var loggedUser = _securityManager.GetLoggedUser();
            _log.Debug("Check list exist in db");
            var item = _db.ToDoListItems.FirstOrDefault(u => u.Id == listItemId && u.User.Id == loggedUser.Id);
            _log.Debug("Check list is null");
            if (item == null)
            {
                _log.ErrorFormat("Item not found");
                throw new ToDoListException(String.Format("Item not found"));
            }
            _toDoListItem = item;

            _log.Debug("Return list");
            return _toDoListItem;
        }

        public IList<ToDoListItem> GetListItems()
        {
            _log.Debug("Value loggedUser");
            var loggedUser = _securityManager.GetLoggedUser();
            _log.Debug("Get all lists belong to user");
            var todolist = from u in _db.ToDoListItems
                           where u.User.Id == loggedUser.Id
                           select u;
            _log.Debug("Return all lists");
            return todolist.ToList();

        }

        public void RemoveList(int listItemId)
        {
            _log.Debug("Value loggedUser");
            var loggedUser = _securityManager.GetLoggedUser();
            _log.Debug("Check list exist in db");
            var list = _db.ToDoListItems.FirstOrDefault(u => u.Id == listItemId && u.User.Id == loggedUser.Id);
            _log.Debug("Check list is not null");
            if (list != null)
            {
                _log.Debug("Remove list");
                _db.Entry<ToDoListItem>(list).State = System.Data.Entity.EntityState.Deleted;
                _db.SaveChanges();
                _log.Info("Removed list");
            }
            else
            {
                _log.ErrorFormat("List with not found");
                throw new ToDoListException(String.Format("List with not found"));
            }
        }

        public void RemoveTaskFromList(int listItemId, int taskId)
        {
            _log.Debug("Check task exist in db");
            var task = _db.ToDoListTasks.FirstOrDefault(u => u.Id == taskId && u.ToDoListItem.Id == listItemId);
            _log.Debug("Check task is not null");
            if (task != null)
            {
                _log.Debug("Remove task");
                _db.Entry<ToDoListTask>(task).State = System.Data.Entity.EntityState.Deleted;
                _db.SaveChanges();
                _log.Info("Removed task");
            }
            else
            {
                _log.ErrorFormat("Task with not found");
                throw new ToDoListException(String.Format("Task with not found"));
            }

        }

        public void UpdateList(ToDoListItem item)
        {
            _log.Debug("Find list in db");
            var list = _db.ToDoListItems.Find(item.Id);
            _log.Debug("Check list is null");
            if (list == null)
            {
                _log.ErrorFormat("List with not found");
                throw new ToDoListException(String.Format("List with not found"));
            }
            _log.Debug("Update list");
            _db.Entry(list).CurrentValues.SetValues(item);
            _db.SaveChanges();
            _log.Info("Updated list");
        }

        public void UpdateTask(ToDoListTask task)
        {
            _log.Debug("Find task in db");
            var taskupdate = _db.ToDoListTasks.Find(task.Id);

            _log.Debug("Check task is null");
            if (taskupdate != null)
            {
                _log.Debug("Update task");
                _db.Entry(taskupdate).CurrentValues.SetValues(task);
                _db.SaveChanges();
                _log.Info("Updated task");
            }
            else
            {
                _log.ErrorFormat("Task with not found");
                throw new ToDoListException(String.Format("Task with not found"));
            }
        }

        public bool CheckIsCompletedTask(string isCompleted)
        {
            _log.Debug("Check task iscompled ");
            if (isCompleted == "Yes")
            {
                return true;
            }
            else if (isCompleted == "No")
            {
                return false;
            }

            return false;
        }
    }

}
