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
        public List<Task> Tasks { get; set; }
        public string[] SelectedTasks { get; set; }

        public ProjectsViewModel()
        {
            Project = new ProjectDto();
        }
    }
}
