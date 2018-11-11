using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using ToDoList.Common.Managers;
using ToDoList.Common.Managers.Log;

namespace ToDoList.WebApi.Auth
{
    public class ToDoListAuthorizeAttribute : AuthorizeAttribute
    {
        //private ILogger _log;
        //private ISecurityManager _securityManager;
        //private ISessionContextManager _sessionContextManager;

        public ToDoListAuthorizeAttribute()
        {
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                //var log = ((ILogManager)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogManager))).GetLogger(typeof(ToDoListAuthorizeAttribute).FullName);
                var sessionContextManager = (ISessionContextManager)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISessionContextManager));
                var securityManager = (ISecurityManager)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISecurityManager));

                if (actionContext.Request.Headers.Authorization != null &&
                    !String.IsNullOrEmpty(actionContext.Request.Headers.Authorization.Parameter))
                {
                    string token = actionContext.Request.Headers.Authorization.Parameter;

                    sessionContextManager.SetSessionToken(token);

                    if (securityManager.GetLoggedUser() != null)
                        return;
                }
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent("You are unauthorized to access this resource")
                };

            }
        }
    }
}