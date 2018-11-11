using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Common.Entities;
using ToDoList.WebApi.Models.ToDoListItem;
using ToDoList.WebApi.Models.ToDoListTask;
using ToDoList.WebApi.Models.User;

namespace ToDoList.WebApi.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ToDoListItemRequest, ToDoListItem>();
                cfg.CreateMap<ToDoListItem, ToDoListItemRequest>();
                cfg.CreateMap<ToDoListTaskRequest, ToDoListTask>();
                cfg.CreateMap<ToDoListTask, ToDoListTaskRequest>();
                cfg.CreateMap<User, UserRequest>();
                cfg.CreateMap<UserRequest, User>();
            });
        }
    }

}