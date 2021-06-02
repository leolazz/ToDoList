using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>();


            CreateMap<ProjectDto, Project>();
               
        }
    }
}
