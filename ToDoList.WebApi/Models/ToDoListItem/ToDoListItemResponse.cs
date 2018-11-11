using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ToDoList.WebApi.Models.ToDoListItem
{
    public class ToDoListItemResponse : BaseResponse
    {
        public object Data { get; set; }
        //public IEnumerable<object> AllToDoList { get; set; }

    }

}