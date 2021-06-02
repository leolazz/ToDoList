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
            return View("Projects");
        }
        public ActionResult New()
        {
            var ProjectViewModel = new ProjectsViewModel();

            var Tasks = _context.Tasks
                .Where(t => t.UserId == _userManager.GetUserId(User))
                .Where(t => t.Completed == false)
                .ToList();

            ProjectViewModel.Tasks = Tasks;
            return View("ProjectForm", ProjectViewModel);
        }
        public ActionResult Save(ProjectsViewModel ProjectViewModel)
        {
            if (ModelState.IsValid)
            {
                var ProjectVM = ProjectViewModel;
                //if (task.Id == 0)
                //{
                //    task.CreatedDate = DateTime.Now;
                //    task.UserId = _userManager.GetUserId(User);
                //}
                //_context.Update(task);
                //_context.SaveChanges();
            }
            return RedirectToAction("Projects", "Home");
        }
    }
}
