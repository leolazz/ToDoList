using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDto>()
                .ForMember(dest =>
                dest.Details,
                opt => opt.MapFrom(src => src.Details))
                .ForMember(dest =>
                dest.Outcomes,
                opt => opt.MapFrom(src => src.Outcomes))
                .ForMember(dest =>
                dest.Qualifiers,
                opt => opt.MapFrom(src => src.Qualifiers));
            CreateMap<TaskDto, Task>()
                .ForMember(dest =>
                dest.Details,
                opt => opt.MapFrom(src => src.Details))
                .ForMember(dest =>
                dest.Outcomes,
                opt => opt.MapFrom(src => src.Outcomes))
                .ForMember(dest =>
                dest.Qualifiers,
                opt => opt.MapFrom(src => src.Qualifiers));
        }
    }
}
