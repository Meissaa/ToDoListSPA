using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoList.Common.Entities;
using ToDoList.Common.Exceptions;
using ToDoList.Common.Managers;
using ToDoList.Common.Managers.Log;
using ToDoList.WebApi.Auth;
using ToDoList.WebApi.Models.ToDoListTask;

namespace ToDoList.WebApi.Controllers
{
    [RoutePrefix(WebApiConfig.API_PREFIX + "/" + WebApiConfig.API_VERSION + "/todolist")]
    [ToDoListAuthorize]
    public class ToDoListTaskController : ApiController
    {

        ILogger _log;
        IToDoListManager _toDoListManager;

        public ToDoListTaskController(ILogManager log, IToDoListManager toDoListManager)
        {
            _log = log.GetLogger(typeof(AuthController).FullName);
            _toDoListManager = toDoListManager;
        }

        [Route("{listId}/task")]
        [HttpPost]
        public ToDoListTaskResponse CreateToDoListTask(int listId, ToDoListTaskRequest item)
        {
            try
            {
                _log.Debug("Create task");
                _toDoListManager.AddTaskToList(listId, Mapper.Map<ToDoListTask>(item));
                _log.Debug("Send result success create task");
                return new ToDoListTaskResponse { StatusCode = 201, Message = "SUCCESS", Data = item };
                _log.Info("Created task");
            }
            catch (ToDoListException ex)
            {
                _log.Error(ex);
                return new ToDoListTaskResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new ToDoListTaskResponse { Message = ex.Message };
            }
        }

        [Route("{listId}/task/{taskId}")]
        [HttpDelete]
        public ToDoListTaskResponse RemoveToDoListTask(int listId, int taskId)
        {
            try
            {
                _log.Debug("Remove task");
                _toDoListManager.RemoveTaskFromList(listId, taskId);
                _log.Debug("Send result success remove task");
                return new ToDoListTaskResponse { StatusCode = 201, Message = "SUCCESS" };
                _log.Info("Removed task");
            }
            catch (ToDoListException ex)
            {
                _log.Error(ex);
                return new ToDoListTaskResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new ToDoListTaskResponse { Message = ex.Message };
            }
        }

        [Route("{listId}/task")]
        [HttpPut]
        public ToDoListTaskResponse UpdateToDoListTask(int listId, ToDoListTaskRequest item)
        {
            try
            {
                //_log.Debug("Check exist task in list ");
                //if (listId != item.ToToDoListItem.Id)
                //{
                //    _log.Debug("Send result not found task");
                //    return new ToDoListTaskResponse { StatusCode = 400, Message = "NOT FOUND" };
                //}
                _log.Debug("Update task");
                _toDoListManager.UpdateTask(Mapper.Map<ToDoListTask>(item));
                _log.Debug("Send result success update task");
                return new ToDoListTaskResponse { StatusCode = 201, Message = "SUCCESS", Data = item };
                _log.Info("Updated task");

            }
            catch (ToDoListException ex)
            {
                _log.Error(ex);
                return new ToDoListTaskResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new ToDoListTaskResponse { Message = ex.Message };
            }
        }

    }
}
