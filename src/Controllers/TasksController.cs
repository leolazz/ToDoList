using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutoMapper;
using ToDoList.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using ToDoList.ViewModels;
using ToDoList.Services;
using ToDoList.Interfaces;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ITaskServices _taskServices;
        public TasksController(UserManager<ApplicationUser> userManager, ITaskServices taskServices)
        {
            _userManager = userManager;
            _taskServices = taskServices;
        }
        public ActionResult Index()
        {
            return View("Index", "Home");   
        }
        public ActionResult New()
        {
           return View("TaskForm", _taskServices.NewTask());
        }
        public ActionResult Edit(int id)
        {
            return View("Edit", _taskServices.EditTask(id, _userManager.GetUserId(User)));
          }
        [HttpPost]
        public ActionResult Save(TaskDto taskDto)
        {
            if (ModelState.IsValid)
                _taskServices.SaveTask(taskDto, _userManager.GetUserId(User));

            return RedirectToAction("GetTasks", "Tasks");
        }
        public ActionResult Delete(int id)
        {
            _taskServices.DeleteTask(id);
            return RedirectToAction("GetTasks");
        }
        public ActionResult GetTasks()
        {
            return View("Doing", _taskServices.GetActiveTasks(_userManager.GetUserId(User)));
        }
        public ActionResult Done()
        {
            return View("Done", _taskServices.GetCompletedTasks(_userManager.GetUserId(User)));
        }
        public ActionResult Search(string searchString)
        {
            return View("Results",  _taskServices.SearchTasks(searchString, _userManager.GetUserId(User)));
        }
    }
}  
