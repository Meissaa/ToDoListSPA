using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.WebApi.Models.ToDoListTask
{
    public class ToDoListTaskResponse : BaseResponse
    {
        public object Data { get; set; }
    }
}