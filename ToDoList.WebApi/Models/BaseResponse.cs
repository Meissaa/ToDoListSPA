using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.WebApi.Models
{
    public class BaseResponse
    { 
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}