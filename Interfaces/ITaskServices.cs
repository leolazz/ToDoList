using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DTOs;
using ToDoList.ViewModels;

namespace ToDoList.Interfaces
{
    public interface ITaskServices
    {
        TaskDto NewTask();
        TaskDto EditTask(int id, string userId);
        void SaveTask(TaskDto taskDto, string userId);
        void DeleteTask(int id);
        TasksViewModel GetActiveTasks(string userId);
        IEnumerable<TaskDto> GetCompletedTasks(string userId);
        TasksViewModel SearchTasks(string searchString, string userId);


    }

}
