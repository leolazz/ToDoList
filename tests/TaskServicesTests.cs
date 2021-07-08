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
    public class TaskServicesTests
    {
        public MapperConfiguration getMaps()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile<ProjectProfile>();
            cfg.AddProfile<TaskDetailsProfile>();
            cfg.AddProfile<TaskOutcomesProfile>();
            cfg.AddProfile<TaskProfile>();
            cfg.AddProfile<TaskQualifiersProfile>();

            var mapperConfig = new MapperConfiguration(cfg);
            return mapperConfig;
        }
        public TaskDto GetSingleTaskDto()
        {
            var task = new TaskDto()
            {
                CreatedDate = DateTime.Now,
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
            using (var factory = new SampleDbContextFactory())
            {
                using (var context = factory.CreateContext())
                {
                    var mapper = new Mapper(getMaps());
                    using (var ts = new TaskServiceTestImplementation(context, mapper))
                    {
                        var task = GetSingleTaskDto();
                        
                        ts.SaveTask(task, null);
                    }
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
        [Fact]
        public void basictest()
        {
            using (var factory = new SampleDbContextFactory())
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
