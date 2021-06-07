using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.DTOs;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IMapper _mapper;
        private SQLiteDBContext _context;
        private UserManager<ApplicationUser> _userManager;
        public ProjectsController(SQLiteDBContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetProjects()
        {
            if (_userManager.GetUserId(User) == null)
            {
               return Redirect("/Identity/Account/Login");
            }
            ProjectsViewModel ProjectVM = new ProjectsViewModel();
                var projects = _context.Projects
                .Where(t => t.UserId == _userManager.GetUserId(User))
                .Where(t => t.Completed == false)
                .Include(t => t.Tasks)
                .ToList();

            ProjectVM.ProjectList = _mapper.Map<List<ProjectDto>>(projects);

                
            return View("Projects", ProjectVM);
        }
        public ActionResult Edit(int id)
        {
            var ProjectViewModel = new ProjectsViewModel();

            var Project = _context.Projects
                .Where(t => t.UserId == _userManager.GetUserId(User))
                .Where(t => t.Completed == false)
                .SingleOrDefault(t => t.Id == id);

            var ProjectTasks = _context.Tasks
                .Where(t => t.Project == Project)
                .Include(t => t.Qualifiers)
                .Include(t => t.Outcomes)
                .Include(t => t.Details)
                .ToList();

            var OrphanedTasks = _context.Tasks
                .Where(t => t.UserId == _userManager.GetUserId(User))
                .Where(t => t.Completed == false)
                .Where(t => t.Project == null)
                .ToList();


            ProjectViewModel.Project = _mapper.Map<ProjectDto>(Project);
            ProjectViewModel.Project.Tasks = _mapper.Map<List<TaskDto>>(ProjectTasks);
            ProjectViewModel.Tasks = _mapper.Map<List<TaskDto>>(OrphanedTasks);

            return View("Edit", ProjectViewModel);
        }
        public ActionResult New()
        {
            if (_userManager.GetUserId(User) == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var ProjectViewModel = new ProjectsViewModel();

            var Tasks = _context.Tasks
                .Where(t => t.UserId == _userManager.GetUserId(User))
                .Where(t => t.Completed == false)
                .Where(t => t.Project == null)
                .ToList();

            ProjectViewModel.Tasks = _mapper.Map<List<TaskDto>>(Tasks);
            return View("ProjectForm", ProjectViewModel);
        }
        public ActionResult Save(ProjectsViewModel ProjectViewModel)
        {
            if (ModelState.IsValid)
            {
                var Project = _mapper.Map<Project>(ProjectViewModel.Project);


                if (Project.Id == 0)
                {
                    Project.CreatedDate = DateTime.Now;
                    Project.UserId = _userManager.GetUserId(User);
                }
                
                _context.Update(Project);
                
                if (!string.IsNullOrEmpty(ProjectViewModel.SelectedTasks))
                {
                    string[] ids = ProjectViewModel.SelectedTasks.Split(',');

                    foreach (var id in ids)
                    {
                        var task = _context.Tasks.FirstOrDefault(t => t.Id == int.Parse(id));
                        task.Project = Project;
                        _context.Update(task);
                    }
                } if (Project.Completed)
                {
                    var tasks = _context.Tasks.Where(t => t.Project == Project).ToList();
                    foreach (var task in tasks)
                    {
                        task.Completed = true;
                        _context.Update(task);
                    }
                }
                _context.SaveChanges(); 
                return RedirectToAction("GetProjects", "Projects");
            }
            return RedirectToAction("Edit", "Projects");
        }
        public ActionResult Done()
        {
            if (_userManager.GetUserId(User) == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            ProjectsViewModel ProjectVM = new ProjectsViewModel();
            var projects = _context.Projects
            .Where(t => t.UserId == _userManager.GetUserId(User))
            .Where(t => t.Completed == true)
            .Include(t => t.Tasks)
            .ToList();

            ProjectVM.ProjectList = _mapper.Map<List<ProjectDto>>(projects);

            return View("Done", ProjectVM);
        }
        public ActionResult Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(t => t.Id == id);
                _context.Remove(project);
            
            var tasks = _context.Tasks.Where(t => t.Project == project).ToList();
            foreach (var task in tasks)
            {
                _context.Remove(task);
            }
            _context.SaveChanges();

            return RedirectToAction("GetProjects");
        }
    }
}
