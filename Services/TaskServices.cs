using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data;
using ToDoList.DTOs;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Services
{
    public class TaskServices : ITaskServices
    {
        private SQLiteDBContext _context;
        private readonly IMapper _mapper;
        public TaskServices(SQLiteDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public TaskDto NewTask()
        {
            return new TaskDto();
        }
        public TaskDto EditTask(int id, string userId)
        {
            Task task = _context.Tasks
                .Where(t => t.UserId == userId)
                .Include(t => t.Qualifiers)
                .Include(t => t.Outcomes)
                .Include(t => t.Details)
                .SingleOrDefault(t => t.Id == id);
            return _mapper.Map<TaskDto>(task);
        }
        public void SaveTask(TaskDto taskDto, string userId)
        {
            var task = _mapper.Map<Task>(taskDto);
            if (task.Id == 0)
            {
                task.CreatedDate = DateTime.Now;
                task.UserId = userId;
            }
            _context.Update(task);
            _context.SaveChanges();
        }
        public void DeleteTask(int id)
        {
            _context.Remove(_context.Tasks.FirstOrDefault(t => t.Id == id));
            _context.SaveChanges();
        }
 
        public TasksViewModel GetActiveTasks(string userId)
        {
            var tasks = _context.Tasks
               .Where(t => t.UserId == userId)
               .Where(t => t.Completed == false)
               .Include(t => t.Qualifiers)
               .Include(t => t.Outcomes)
               .Include(t => t.Details)
               .ToList();
            var viewModel = new TasksViewModel();
            viewModel.taskDto = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return viewModel;
        }

        public IEnumerable<TaskDto> GetCompletedTasks(string userId)
        {
            var task = _context.Tasks
               .Where(t => t.UserId == userId)
               .Where(t => t.Completed == true)
               .Include(t => t.Qualifiers)
               .Include(t => t.Outcomes)
               .Include(t => t.Details)
               .ToList();
            return _mapper.Map<IEnumerable<TaskDto>>(task); 
        }

        public TasksViewModel SearchTasks(string searchString, string userId)
        {
            var tasks = _context.Tasks
               .Where(t => t.UserId == userId)
               .Include(t => t.Qualifiers)
               .Include(t => t.Outcomes)
               .Include(t => t.Details)
               .Where(x => EF.Functions.Like(x.Title, $"%{searchString}%"))
               .ToList();
            TasksViewModel viewModel = new TasksViewModel();
            viewModel.taskDto = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return viewModel;
        }
    }
}
