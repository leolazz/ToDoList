using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Profiles
{
    public class TaskDetailsProfile : Profile
    {
        public TaskDetailsProfile()
        {
            CreateMap<TaskDetails, TaskDetailsDto>();
            CreateMap<TaskDetailsDto, TaskDetails>();
        }
    }
}
