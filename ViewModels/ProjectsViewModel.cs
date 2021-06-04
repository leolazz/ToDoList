using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;
using ToDoList.DTOs;

namespace ToDoList.ViewModels
{
    public class ProjectsViewModel
    {
        public ProjectDto Project { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public string SelectedTasks { get; set; }
        public string[] Colors;
        public ProjectsViewModel()
        {
            Project = new ProjectDto();
            Colors = new string[] { "rgb(255,0,0)", "rgb(255, 0, 0)", "rgb(255,77,0)", "rgb(255,116,0)", "rgb(255,154,0)", "rgb(255,193,0)", "rgb(255, 210, 0)", "rgb(255, 227, 0)" };
        }
    }
}
