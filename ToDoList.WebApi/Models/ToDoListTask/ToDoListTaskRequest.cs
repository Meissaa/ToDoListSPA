using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.WebApi.Models.ToDoListItem;

namespace ToDoList.WebApi.Models.ToDoListTask
{
    public class ToDoListTaskRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public ToDoListItemRequest ToToDoListItem { get; set; }
    }
}