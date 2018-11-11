using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ToDoList.WebAppSpa.Startup))]

namespace ToDoList.WebAppSpa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
