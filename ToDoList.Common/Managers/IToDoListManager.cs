using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Entities;

namespace ToDoList.Common.Managers
{
    public interface IToDoListManager : IDisposable
    {
        void CreateList(ToDoListItem item);
        void UpdateList(ToDoListItem item);
        void RemoveList(int listItemId);
        IList<ToDoListItem> GetListItems();
        ToDoListItem GetListItemDetail(int listItemId);
        void AddTaskToList(int listItemId, ToDoListTask task);
        void UpdateTask(ToDoListTask task);
        void RemoveTaskFromList(int listItemId, int taskId);
        bool CheckIsCompletedTask(string isCompleted);

    }
}
