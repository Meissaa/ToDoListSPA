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
using ToDoList.WebApi.Models.ToDoListItem;

namespace ToDoList.WebApi.Controllers
{
    [RoutePrefix(WebApiConfig.API_PREFIX + "/" + WebApiConfig.API_VERSION + "/todolist")]
    [ToDoListAuthorize]
    public class ToDoListController : ApiController
    {
        ILogger _log;
        IToDoListManager _toDoListManager;

        public ToDoListController(ILogManager log, IToDoListManager toDoListManager)
        {
            _log = log.GetLogger(typeof(AuthController).FullName);
            _toDoListManager = toDoListManager;
        }

        [HttpGet]
        public ToDoListItemResponse GetAllLists()
        {
            try
            {
                _log.Debug("Get all lists");
                var allLists = _toDoListManager.GetListItems();
                _log.Debug("Check value allList is null");
                if (allLists == null)
                {
                    _log.Debug("Send result not found lists");
                    return new ToDoListItemResponse { StatusCode = 404, Message = "NOT FOUND" };
                }
                _log.Debug("Send result success get all lists");
                return new ToDoListItemResponse { StatusCode = 200, Message = "SUCCESS", Data = allLists };
            }
            catch (ToDoListException ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
        }

        [Route("{id}")]
        [HttpGet]
        public ToDoListItemResponse GetDetails(int id)
        {
            try
            {
                _log.Debug("Get listItemDetails");
                var listItemDetail = _toDoListManager.GetListItemDetail(id);
                _log.Debug("Check value listItemDetail is null");
                if (listItemDetail == null)
                {
                    _log.Debug("Send result not found listItemDetail");
                    return new ToDoListItemResponse { StatusCode = 404, Message = "NOT FOUND" };
                }
                _log.Debug("Send result success get listItemDetails");
                return new ToDoListItemResponse { StatusCode = 200, Message = "SUCCESS", Data = listItemDetail };
            }
            catch (ToDoListException ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
        }

        [HttpPost]
        public ToDoListItemResponse CreateToDoListItem(ToDoListItemRequest item)
        {
            try
            {
                _log.Debug("Create list");
                _toDoListManager.CreateList(Mapper.Map<ToDoListItem>(item));
                _log.Debug("Send result success create list");
                return new ToDoListItemResponse { StatusCode = 201, Message = "SUCCESS", Data = item };
                _log.Info("Created list");
            }
            catch (ToDoListException ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
        }

        [HttpPut]
        public ToDoListItemResponse UpdateToDoListItem(ToDoListItemRequest item)
        {
            try
            {
                _log.Debug("Update list");
                _toDoListManager.UpdateList(Mapper.Map<ToDoListItem>(item));
                _log.Debug("Send result success update list");
                return new ToDoListItemResponse { StatusCode = 201, Message = "SUCCESS", Data = item };
                _log.Info("Updated list");
            }
            catch (ToDoListException ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public ToDoListItemResponse RemoveToDoListItem(int id)
        {
            try
            {
                _log.Debug("Remove list");
                _toDoListManager.RemoveList(id);
                _log.Debug("Send result success remove list");
                return new ToDoListItemResponse { StatusCode = 201, Message = "SUCCESS" };
                _log.Info("Removed list");
            }
            catch (ToDoListException ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return new ToDoListItemResponse { Message = ex.Message };
            }
        }

    }
}
