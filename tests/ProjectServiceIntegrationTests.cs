using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Services;
using ToDoList.Models;
using AutoMapper;
using ToDoList.Profiles;
using AutoMapper.Configuration;
using ToDoList.DTOs;
using Xunit;
using ToDoList.ViewModels;

namespace ToDoList.Tests
{
    public class ProjectServiceIntegrationTests
    {
        public Mapper GetMapper()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile<ProjectProfile>();
            cfg.AddProfile<TaskDetailsProfile>();
            cfg.AddProfile<TaskOutcomesProfile>();
            cfg.AddProfile<TaskProfile>();
            cfg.AddProfile<TaskQualifiersProfile>();
            cfg.AddProfile<ProjectProfile>();

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
        public ApplicationUser GetApplicationUser(TestDbContext.TestSQLiteDBContext context)
        {
            return new ApplicationUser()
            {
                Id = "5517ec97-5a64-487e-94a3-ac3b5bca5274",
                AccessFailedCount = 0,
                ConcurrencyStamp = "ee01e2c9-39cc-4358-a7a8-1c38dffb81fc",
                Email = "leolazzarini101@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                NormalizedEmail = "LEOLAZZARINI101@GMAIL.COM",
                NormalizedUserName = "LEOLAZZARINI101@GMAIL.COM",
                SecurityStamp = "WXFCSB3JKJTMBXMLCWPMHEHAUKGDQPZP",
                UserName = "leolazzarini101@gmail.com"
            };
        }
        public ProjectsViewModel GetProjectsViewModel()
        {
            var vm = new ProjectsViewModel()
            {
                SelectedTasks = "1, 2"
            };
            return vm;
        }
        [Fact]
        public void NewProject_Returns_New_ProjectViewModel()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                {
                    var ps = new ProjectService(context, GetMapper());
                    var vm = ps.NewProject(null);
                    var expected = typeof(ProjectsViewModel);

                    Assert.IsType(expected, vm);
                    Assert.NotNull(vm.Project);
                    Assert.NotNull(vm.Tasks);

                }
            }
        }
        [Fact]
        public void SaveProject_Saves_New_Project_And_Tags_Task_With_Id()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                { 
                    context.ApplicationUsers.Add(GetApplicationUser(context));
                    var ps = new ProjectService(context, GetMapper());
                    var ts = new TaskService(context, GetMapper());
                    ts.SaveTask(GetSingleTaskDto(), null);
                    ts.SaveTask(GetSingleTaskDto(), null);

                    var vm = GetProjectsViewModel();
                    ps.SaveProject(vm, "5517ec97-5a64-487e-94a3-ac3b5bca5274");

                    var mapper = GetMapper();
                    
                    var expected = typeof(ProjectsViewModel);
                    var activeProjectTasks = ts.GetActiveTasks("5517ec97-5a64-487e-94a3-ac3b5bca5274");

                    
                    var projectFromDb = context.Projects.FirstOrDefault(p => p.Completed == false);

                    activeProjectTasks.taskDto.Where(t => t.Project == projectFromDb);
                    Assert.NotNull(projectFromDb);
                    foreach (var task in activeProjectTasks.taskDto)
                    {
                        Assert.Equal(projectFromDb, task.Project);
                    }

                }
            }
        }
    }
}
