using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.ViewModels;

namespace ToDoList.Interfaces
{
    public interface IProjectServices
    {
        ProjectsViewModel NewProject(string userId);
        ProjectsViewModel EditProject(int id, string userId);
        void SaveProject(ProjectsViewModel projectVM, string userId);
        void DeleteProject(int id);
        ProjectsViewModel GetActiveProjects(string userId);
        ProjectsViewModel GetCompletedProjects(string userId);
    }
}
