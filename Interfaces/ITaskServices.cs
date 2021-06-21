using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DTOs;

namespace ToDoList.Interfaces
{
    public interface ITaskServices
    {
        TaskDto NewTask();
        TaskDto EditTask(int id, string userId);
        void SaveTask(TaskDto taskDto);
        void DeleteTask(int id);
        IEnumerable<TaskDto> GetActiveTasks();
        IEnumerable<TaskDto> GetCompletedTasks();
        IEnumerable<TaskDto> SearchTasks(string searchString);


    }

}
