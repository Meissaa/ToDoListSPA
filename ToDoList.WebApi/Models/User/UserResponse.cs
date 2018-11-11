using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.WebApi.Models.User
{
    public class UserResponse : BaseResponse
    {
        public object Data { get; set; }
    }
}