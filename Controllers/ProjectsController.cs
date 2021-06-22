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
        private IProjectServices _projectServices;
        private UserManager<ApplicationUser> _userManager;
        public ProjectsController(IProjectServices projectServices, UserManager<ApplicationUser> userManager)
        {
            _projectServices = projectServices;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult New()
        {
            if (_userManager.GetUserId(User) == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            return View("ProjectForm", _projectServices.NewProject(_userManager.GetUserId(User)));
        }
        public ActionResult Edit(int id)
        {
            return View("Edit", _projectServices.EditProject(id, _userManager.GetUserId(User)));
        }
        public ActionResult Save(ProjectsViewModel projectVM)
        {
            if (ModelState.IsValid)
            {
                _projectServices.SaveProject(projectVM, _userManager.GetUserId(User));
                return RedirectToAction("GetProjects", "Projects");
            }
            return RedirectToAction("Edit", "Projects");
        }
        public ActionResult Delete(int id)
        {
            _projectServices.DeleteProject(id);
            return RedirectToAction("GetProjects");
        }
        public ActionResult GetProjects()
        {
            if (_userManager.GetUserId(User) == null)
            {
               return Redirect("/Identity/Account/Login");
            }
            return View("Projects", _projectServices.GetActiveProjects(_userManager.GetUserId(User)));
        }
        public ActionResult Done()
        {
            if (_userManager.GetUserId(User) == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            return View("Done", _projectServices.GetCompletedProjects(_userManager.GetUserId(User)));
        }
    }
}
