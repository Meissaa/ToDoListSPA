using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http;
using ToDoList.Common.Managers;
using ToDoList.Common.Managers.Log;
using ToDoList.WebApi.Auth;
using ToDoList.WebApi.Models.Auth;

namespace ToDoList.WebApi.Controllers
{
   
    [RoutePrefix(WebApiConfig.API_PREFIX + "/" + WebApiConfig.API_VERSION + "/auth")]
    [ToDoListAuthorize]
    public class AuthController : ApiController
    {
        ILogger _log;
        ISecurityManager _securityManager;
        public AuthController(ILogManager log, ISecurityManager securityManager)
        {
            _log = log.GetLogger(typeof(AuthController).FullName);
            _securityManager = securityManager;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public LoginResponse Login(LoginRequest request)
        {
            try
            {
                _log.Debug("Value token");
                string token = _securityManager.Login(request.Username, request.Password);
                _log.Debug("Check token is empty or null");
                if (string.IsNullOrEmpty(token))
                {
                    _log.Debug("Login failed.Token is empty or null.");
                    return new LoginResponse { StatusCode = 401, Message = "FAILED" };
                }
                _log.Debug("Login success.Token is not empty or null.");
                return new LoginResponse { StatusCode = 200, Message = "SUCCESS", SessionToken = token };
                _log.Info("User logged.");
            }
            catch (SecurityException ex)
            {
                _log.Error(ex);
                return new LoginResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new LoginResponse { Message = ex.Message };
            }
        }

        [Route("logout")]
        [HttpPost]
        [AllowAnonymous]
        public LogoutResponse Logout(LogoutRequest request)
        {
            LogoutResponse logoutResponse = null;
            try
            {
                _log.Debug("Logout user");
                _securityManager.Logout();
                _log.Debug("Check user is logged");
                if (!_securityManager.IsUserLogged())
                {
                    _log.Debug("Send result success user logout");
                    return new LogoutResponse { StatusCode = 200, Message = "SUCCESS" };
                    _log.Info("User logout");
                }

                _log.Debug("Check user is not logged");
                return logoutResponse;
            }
            catch (SecurityException ex)
            {
                _log.Error(ex);
                return new LogoutResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new LogoutResponse { Message = ex.Message };
            }
        }
    }
}
