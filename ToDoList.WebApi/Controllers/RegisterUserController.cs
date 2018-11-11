using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoList.Common.Entities;
using ToDoList.Common.Exceptions.Security;
using ToDoList.Common.Managers;
using ToDoList.Common.Managers.Log;
using ToDoList.WebApi.Auth;
using ToDoList.WebApi.Models.User;

namespace ToDoList.WebApi.Controllers
{
    [RoutePrefix(WebApiConfig.API_PREFIX + "/" + WebApiConfig.API_VERSION + "/registeruser")]
    [ToDoListAuthorize]
    public class RegisterUserController : ApiController
    {
        ILogger _log;
        IUserManager _userManager;
        public RegisterUserController(ILogManager log, IUserManager userManager)
        {
            _log = log.GetLogger(typeof(AuthController).FullName);
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public UserResponse Register(UserRequest user)
        {
            try
            {
                _log.Debug("Create user");
                _userManager.Create(Mapper.Map<User>(user));
                _log.Debug("Send result success create user");
                return new UserResponse { StatusCode = 201, Message = "SUCCESS", Data = user};
                _log.Info("Created user.");
            }
            catch (CreateUserFailedException ex)
            {
                _log.Error(ex);
                return new UserResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new UserResponse { Message = ex.Message };
            }
        }

    }
}
