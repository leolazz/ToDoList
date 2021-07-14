using System;
using Xunit;
using ToDoList.Services;
using ToDoList.Models;
using System.Linq;
using AutoMapper;
using ToDoList.Profiles;
using AutoMapper.Configuration;
using ToDoList.DTOs;

namespace ToDoList.Tests
{
    public class TaskServicesIntegrationTests
    {
        public Mapper GetMapper()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile<ProjectProfile>();
            cfg.AddProfile<TaskDetailsProfile>();
            cfg.AddProfile<TaskOutcomesProfile>();
            cfg.AddProfile<TaskProfile>();
            cfg.AddProfile<TaskQualifiersProfile>();

            var mapperConfig = new MapperConfiguration(cfg);
            var mapper = new Mapper(mapperConfig);
            return mapper;
        }
        public TaskDto GetSingleTaskDto()
        {
            var task = new TaskDto()
            {
                Title = "test",
                Details = new TaskDetailsDto() { Details = "test" },
                Qualifiers = new TaskQualifiersDto() { Qualifiers = "test" },
                Outcomes = new TaskOutcomesDto() { Outcomes = "test" },
                Completed = false,
                EndDate = DateTime.Now.AddDays(1),
            };
            return task;
        }
        [Fact]
        public void NewTask_Returns_New_TaskDto()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                {
                    var ts = new TaskService(context, GetMapper());
                    var taskdto = ts.NewTask();
                    Assert.IsType<TaskDto>(taskdto);
                }
            }
        }
        [Fact]
        public void SaveTask_Saves_Task_And_GetActiveTasks_Retrieves_Task()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                {
                    var ts = new TaskService(context, GetMapper());
                    var task = GetSingleTaskDto();
                    ts.SaveTask(task, null);
                }
                using (var context = factory.CreateContext())
                {
                    var ts = new TaskService(context, GetMapper());
                    var vm = ts.GetActiveTasks(null);
                    var task = vm.taskDto.FirstOrDefault(t => t.Title == "test");
                    Assert.Equal("test", task.Title);
                }
            }
        }
        [Fact]
        public void GetActiveTasks_Retrieves_Only_Active_Tasks()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                {
                    var ts = new TaskService(context, GetMapper());
                    var task = GetSingleTaskDto();
                    task.Title = "Active 1";
                    task.Completed = true;
                    var task2 = GetSingleTaskDto();
                    task2.Title = "Active 2";
                    task2.Completed = true;

                    ts.SaveTask(task, null);
                    ts.SaveTask(task2, null);
                    var activeTasks = ts.GetActiveTasks(null);
                    foreach (var taskDto in activeTasks.taskDto)
                    {
                        Assert.True(taskDto.Completed);
                    }
                }
            }
        }
        [Fact]
        public void SearchTasks_Returns_Tasks_Only_Where_Title_Includes_SearchString()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                {
                    var ts = new TaskService(context, GetMapper());
                    var task = GetSingleTaskDto();
                    task.Title = "SearchString";
                    var task1 = GetSingleTaskDto();

                    ts.SaveTask(task, null);
                    ts.SaveTask(task1, null);
                    var vm = ts.SearchTasks("SearchString", null);
                    var searchstringtest = vm.taskDto.FirstOrDefault();
                    var count = vm.taskDto.Count();
                    Assert.Equal("SearchString", searchstringtest.Title);
                    Assert.Equal(1, count);
                }
            }
        }
        [Fact]
        public void DeleteTask_Deletes_Tasks()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                {
                    var ts = new TaskService(context, GetMapper());
                    var task = GetSingleTaskDto();
                    task.Title = "tobedeleted";
                    ts.SaveTask(task, null);
                        
                    var persistedTask = ts.EditTask(1, null);
                    Assert.Equal("tobedeleted", persistedTask.Title);
                    Assert.Equal(1, persistedTask.Id);
                    ts.DeleteTask(1);
                    var deleted = ts.EditTask(1, null);
                    Assert.Null(deleted);
                }
            }
        }
        [Fact]
        public void basictest()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                {
                    var task = new Task()
                    {
                        CreatedDate = DateTime.Now,
                        Title = "test",
                        Details = new TaskDetails() { Details = "test" },
                        Qualifiers = new TaskQualifiers() { Qualifiers = "test" },
                        Outcomes = new TaskOutcomes() { Outcomes = "test" },
                        Completed = false,
                        EndDate = DateTime.Now.AddDays(1),
                    };
                    context.Tasks.Add(task);
                    context.SaveChanges();
                }
                using (var context = factory.CreateContext())
                {
                    var count = context.Tasks.Count();
                    Assert.Equal(1, count);

                    var t = context.Tasks.FirstOrDefault(task => task.Title == "test");
                    Assert.NotNull(t);
                }
            }
        }
    }
}
