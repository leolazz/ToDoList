using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DTOs;

namespace ToDoList.ViewModels
{
    public class TasksViewModel
    {
        public IEnumerable<TaskDto> taskDto { get; set; }

        public string[] Colors;

        public TasksViewModel()
        {
            Colors = new string[] { "rgb(255,0,0)", "rgb(255, 0, 0)", "rgb(255,77,0)", "rgb(255,116,0)", "rgb(255,154,0)", "rgb(255,193,0)", "rgb(255, 210, 0)", "rgb(255, 227, 0)" };
        }
    }
}
