﻿using Microsoft.AspNetCore.Mvc;
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
            if (ModelState.IsValid)
            {
                taskDto.CreatedDate = DateTime.Now;
                var task = _mapper.Map<Task>(taskDto);
                _context.Add(task);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetTasks()
        {
            
            var tasks = _context.Tasks.Include(t => t.Qualifiers).Include(t => t.Outcomes).ToList();
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return View("Doing", tasksDto);
                
        }
        public ActionResult Search(string searchString)
        {
            // There might be a more efficient way of doing this. This could be an issue depending on the collation of the DB
            var task = _context.Tasks.Include(t => t.Qualifiers).Include(t => t.Outcomes).Where(x => EF.Functions.Like(x.Title, $"%{searchString}%")).ToList();
            var taskDto = _mapper.Map<IEnumerable<TaskDto>>(task);
            return View("Doing",  taskDto);
        }
    }
}  
