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
using ToDoList.Services;
using ToDoList.Interfaces;

namespace ToDoList.Controllers
{
    public class ProjectsController : Controller
    {
        private IProjectService _projectservice;
        private UserManager<ApplicationUser> _userManager;
        public ProjectsController(IProjectService projectService, UserManager<ApplicationUser> userManager)
        {
            _projectservice = projectService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult New()
        {
            if (_userManager.GetUserId(User) == null)
                return Redirect("/Identity/Account/Login");
            
            return View("ProjectForm", _projectservice.NewProject(_userManager.GetUserId(User)));
        }
        public ActionResult Edit(int id)
        {
            return View("Edit", _projectservice.EditProject(id, _userManager.GetUserId(User)));
        }
        public ActionResult Save(ProjectsViewModel projectVM)
        {
            if (ModelState.IsValid)
            {
                _projectservice.SaveProject(projectVM, _userManager.GetUserId(User));
                return RedirectToAction("GetProjects", "Projects");
            }
            return RedirectToAction("GetProjects", "Projects");
        }
        public ActionResult Delete(int id)
        {
            _projectservice.DeleteProject(id);
            return RedirectToAction("GetProjects");
        }
        public ActionResult GetProjects()
        {
            if (_userManager.GetUserId(User) == null)
               return Redirect("/Identity/Account/Login");

            return View("Projects", _projectservice.GetActiveProjects(_userManager.GetUserId(User)));
        }
        public ActionResult Done()
        {
            if (_userManager.GetUserId(User) == null)
                return Redirect("/Identity/Account/Login");
            
            return View("Done", _projectservice.GetCompletedProjects(_userManager.GetUserId(User)));
        }
    }
}
