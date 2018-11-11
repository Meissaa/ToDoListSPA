using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Common.Entities;
using ToDoList.WebApi.Models.ToDoListTask;
using ToDoList.WebApi.Models.User;

namespace ToDoList.WebApi.Models.ToDoListItem
{
    public class ToDoListItemRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

    }
}