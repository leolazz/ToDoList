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

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        private readonly IMapper _mapper;
        private SQLiteDBContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TasksController(SQLiteDBContext context, IMapper mapper, UserManager<ApplicationUser> userManager  )
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public class test
        {
            public string teststring { get; set; }
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
        public ActionResult Edit(int id)
        {
            Task task = _context.Tasks
                .Where(t => t.UserId == _userManager.GetUserId(User))
                .Include(t => t.Qualifiers)
                .Include(t => t.Outcomes)
                .Include(t => t.Details)
                .SingleOrDefault(t => t.Id == id);
            
            return View("Edit", _mapper.Map<TaskDto>(task));
        }

        [HttpPost]
        public ActionResult Save(TaskDto taskDto)
        {
            
            if (ModelState.IsValid)
            {
                var task = _mapper.Map<Task>(taskDto);
                if(task.Id == 0)
                {
                    task.CreatedDate = DateTime.Now;
                    task.UserId = _userManager.GetUserId(User);
                }
                _context.Update(task);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
      

        public ActionResult GetTasks()
        {
            var tasks = _context.Tasks
                .Where(t => t.UserId == _userManager.GetUserId(User))
                .Where(t => t.Completed == false)
                .Include(t => t.Qualifiers)
                .Include(t => t.Outcomes)
                .Include(t => t.Details)
                .ToList();
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return View("Doing", tasksDto);
                
        }
        public ActionResult Search(string searchString)
        {
            // There might be a more efficient way of doing this. This could be an issue depending on the collation of the DB
            var task = _context.Tasks
                .Where(t => t.UserId == _userManager.GetUserId(User))
                .Include(t => t.Qualifiers)
                .Include(t => t.Outcomes)
                .Include(t => t.Details)
                .Where(x => EF.Functions.Like(x.Title, $"%{searchString}%"))
                .ToList();
            var taskDto = _mapper.Map<IEnumerable<TaskDto>>(task);
            return View("Results",  taskDto);
        }
        public ActionResult Done()
        {
            var task = _context.Tasks
                .Where(t => t.UserId == _userManager.GetUserId(User))
                .Where(t => t.Completed == true)
                .Include(t => t.Qualifiers)
                .Include(t => t.Outcomes)
                .Include(t => t.Details)
                .ToList();
            var taskDto = _mapper.Map<IEnumerable<TaskDto>>(task);
            return View("Done", taskDto);
        }
        
    }
}  
