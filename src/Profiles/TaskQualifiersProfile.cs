using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Profiles
{
    public class TaskQualifiersProfile : Profile
    {
        public TaskQualifiersProfile()
        {
            CreateMap<TaskQualifiers, TaskQualifiersDto>();
            CreateMap<TaskQualifiersDto, TaskQualifiers>();
        }
    }
}
