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
        private ITaskService _taskService;
        public TasksController(UserManager<ApplicationUser> userManager, ITaskService taskService)
        {
            _userManager = userManager;
            _taskService = taskService;
        }
        public ActionResult Index()
        {
            return View("Index", "Home");   
        }
        public ActionResult New()
        {
           return View("TaskForm", _taskService.NewTask());
        }
        public ActionResult Edit(int id)
        {
            return View("Edit", _taskService.EditTask(id, _userManager.GetUserId(User)));
          }
        [HttpPost]
        public ActionResult Save(TaskDto taskDto)
        {
            if (ModelState.IsValid)
                _taskService.SaveTask(taskDto, _userManager.GetUserId(User));

            return RedirectToAction("GetTasks", "Tasks");
        }
        public ActionResult Delete(int id)
        {
            _taskService.DeleteTask(id);
            return RedirectToAction("GetTasks");
        }
        public ActionResult GetTasks()
        {
            return View("Doing", _taskService.GetActiveTasks(_userManager.GetUserId(User)));
        }
        public ActionResult Done()
        {
            return View("Done", _taskService.GetCompletedTasks(_userManager.GetUserId(User)));
        }
        public ActionResult Search(string searchString)
        {
            return View("Results",  _taskService.SearchTasks(searchString, _userManager.GetUserId(User)));
        }
    }
}  
