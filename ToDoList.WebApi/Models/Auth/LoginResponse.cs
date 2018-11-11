using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.WebApi.Models.Auth
{
    public class LoginResponse : BaseResponse
    {
        public string SessionToken { get; set; }
    }
}