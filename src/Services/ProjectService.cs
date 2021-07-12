using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.DTOs;
using ToDoList.Interfaces;
using ToDoList.ViewModels;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ProjectService : IProjectService
    {
        private ToDoListDbContext _context;
        private readonly IMapper _mapper;
        public ProjectService(ToDoListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ProjectsViewModel NewProject(string userId)
        {
            var ProjectVM = new ProjectsViewModel();
            var Tasks = _context.Tasks
                .Where(t => t.UserId == userId)
                .Where(t => t.Completed == false)
                .Where(t => t.Project == null)
                .ToList();
            ProjectVM.Tasks = _mapper.Map<List<TaskDto>>(Tasks);
            return ProjectVM;
        }
        public ProjectsViewModel EditProject(int id, string userId)
        {
            var Project = _context.Projects
                .Where(t => t.UserId == userId)
                .Where(t => t.Completed == false)
                .SingleOrDefault(t => t.Id == id);
            var ProjectTasks = _context.Tasks
                .Where(t => t.Project == Project)
                .Include(t => t.Qualifiers)
                .Include(t => t.Outcomes)
                .Include(t => t.Details)
                .ToList();
            var OrphanedTasks = _context.Tasks
                .Where(t => t.UserId == userId)
                .Where(t => t.Completed == false)
                .Where(t => t.Project == null)
                .ToList();
            var ProjectVM = new ProjectsViewModel()
            {
                Project = _mapper.Map<ProjectDto>(Project),
                Tasks = _mapper.Map<List<TaskDto>>(OrphanedTasks)
            };
            ProjectVM.Project.Tasks = _mapper.Map<List<TaskDto>>(ProjectTasks); // Error here when saving a project without selecting date
            return ProjectVM;   
        }
        public void SaveProject(ProjectsViewModel projectVM, string userId)
        {
            var Project = _mapper.Map<Project>(projectVM.Project);
            if (Project.Id == 0)
            {
                Project.CreatedDate = DateTime.Now;
                Project.UserId = userId;
            }
            // Changed to resolve exception that showed only in testing for tracking multiple entities with same id
            _context.Entry(Project).CurrentValues.SetValues(Project);
            _context.SaveChanges();  
            if (!string.IsNullOrEmpty(projectVM.SelectedTasks))
            {
                string[] ids = projectVM.SelectedTasks.Split(',');
                foreach (var id in ids)
                {
                    var task = _context.Tasks.FirstOrDefault(t => t.Id == int.Parse(id));
                    task.Project = Project;
                    _context.Update(task);
                }
            }
            if (Project.Completed)
            {
                var tasks = _context.Tasks.Where(t => t.Project == Project).ToList();
                foreach (var task in tasks)
                {
                    task.Completed = true;
                    _context.Update(task);
                }
            }
            _context.SaveChanges();
        }
        public void DeleteProject(int id)
        {
            var project = _context.Projects.FirstOrDefault(t => t.Id == id);
            _context.Remove(project);
            var tasks = _context.Tasks.Where(t => t.Project == project).ToList();
            foreach (var task in tasks)
            {
                _context.Remove(task);
            }
            _context.SaveChanges();
        }
        public ProjectsViewModel GetActiveProjects(string userId)
        {
            var ProjectVM = new ProjectsViewModel();
            ProjectVM.ProjectList = _mapper.Map<List<ProjectDto>>(_context.Projects
            .Where(t => t.UserId == userId)
            .Where(t => t.Completed == false)
            .Include(t => t.Tasks)
            .ToList());
            return ProjectVM;
        }
        public ProjectsViewModel GetCompletedProjects(string userId)
        {
            ProjectsViewModel ProjectVM = new ProjectsViewModel();
            ProjectVM.ProjectList = _mapper.Map<List<ProjectDto>>(_context.Projects
            .Where(t => t.UserId == userId)
            .Where(t => t.Completed == true)
            .Include(t => t.Tasks)
            .ToList());
            return ProjectVM;
        }
    }
}
