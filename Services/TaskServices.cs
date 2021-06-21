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

namespace ToDoList.Services
{
    public class TaskServices : ITaskServices
    {
        private SQLiteDBContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public TaskServices(SQLiteDBContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
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
        public void SaveTask(TaskDto taskDto)
        {
            throw new NotImplementedException();
        }
        public void DeleteTask(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TaskDto> GetActiveTasks()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TaskDto> GetCompletedTasks()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TaskDto> SearchTasks(string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
