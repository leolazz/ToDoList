using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ToDoList.DTOs;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        private readonly IMapper _mapper;
        private ApplicationDbContext _context;
        public TasksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public ActionResult Index()
        {
            return View("Index", "Home");

        }

        public ActionResult New()
        {
            var Task = new Task();

            TaskDto taskDto = _mapper.Map<TaskDto>(Task);
            
           return View("TaskForm", taskDto);
        }
        [HttpPost]
        public ActionResult Save(TaskDto taskDto)
        {
            var task = _mapper.Map<Task>(taskDto);

            _context.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetTasks()
        {
            var Tasks = _context.Tasks.Include(t => t.Qualifiers).Include(t => t.Outcomes).SingleOrDefault(t => t.Id == 3);
            var taskDto = _mapper.Map<TaskDto>(Tasks);
            return View(taskDto);
        }
    }
}  
