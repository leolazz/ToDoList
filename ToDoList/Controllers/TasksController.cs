using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        private ApplicationDbContext _context;
        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public ActionResult Index()
        {
            return View("Index", "Home");

        }

        public ActionResult New()
        {
            var Task = new Task();
            
           return View("TaskForm", Task);
        }
        [HttpPost]
        public ActionResult Save(Task task)
        {
            _context.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index", "Tasks");
        }
    }
}  
