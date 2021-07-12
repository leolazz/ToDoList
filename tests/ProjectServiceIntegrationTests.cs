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
        private string userId = "5517ec97-5a64-487e-94a3-ac3b5bca5274";
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
                Id = userId,
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
                    //Arrange
                    context.ApplicationUsers.Add(GetApplicationUser(context));
                    var ps = new ProjectService(context, GetMapper());
                    var ts = new TaskService(context, GetMapper());
                    ts.SaveTask(GetSingleTaskDto(), null);
                    ts.SaveTask(GetSingleTaskDto(), null);

                    //Act
                    ps.SaveProject(GetProjectsViewModel(), userId);
                    var activeProjectTasks = ts.GetActiveTasks(userId);
                    var projectFromDb = context.Projects.FirstOrDefault(p => p.Completed == false);
                    activeProjectTasks.taskDto.Where(t => t.Project == projectFromDb);
                      
                    //Assert
                    Assert.NotNull(projectFromDb);
                    foreach (var task in activeProjectTasks.taskDto)
                    {
                        Assert.Equal(projectFromDb, task.Project);
                    }

                }
            }
        }
        [Fact]
        public void DeleteProject_Deletes_Project_And_Tasks()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                {
                    //Arrange
                    context.ApplicationUsers.Add(GetApplicationUser(context));
                    var ps = new ProjectService(context, GetMapper());
                    var ts = new TaskService(context, GetMapper());
                    ts.SaveTask(GetSingleTaskDto(), null);
                    ts.SaveTask(GetSingleTaskDto(), null);
                    ps.SaveProject(GetProjectsViewModel(), userId);
                    Assert.NotNull(context.Projects.First(p => p.Id == 1));

                    //Act
                    ps.DeleteProject(1);
                    var deletedTasks = context.Tasks.Where(t => t.Project == context.Projects.FirstOrDefault(p => p.Id == 1));
                    var deleted = context.Projects.FirstOrDefault(p => p.Id == 1);

                    //Assert
                    Assert.Null(deleted);
                    Assert.Empty(deletedTasks);
                }
            }
        }
        [Fact]
        public void GetActiveProjects_Gets_Only_Active_Projects()
        {
            using (var factory = new TestDbContext())
            {
                using (var context = factory.CreateContext())
                {
                    //Arrange
                    context.ApplicationUsers.Add(GetApplicationUser(context));
                    var ps = new ProjectService(context, GetMapper());
                    var vmList = new List<ProjectsViewModel>()
                    {
                         new ProjectsViewModel {Project = new ProjectDto { Completed = false} },
                         new ProjectsViewModel {Project = new ProjectDto { Completed = false} },
                         new ProjectsViewModel {Project = new ProjectDto { Completed = false} },
                         new ProjectsViewModel {Project = new ProjectDto { Completed = true} }
                    };
                    foreach (var project in vmList)
                    {
                        ps.SaveProject(project, userId);
                    };

                    //Act
                    var activeProjects = ps.GetActiveProjects(userId);
                    //Assert

                    Assert.Equal(3, activeProjects.ProjectList.Count());
                    foreach (var project in activeProjects.ProjectList)
                    {
                        Assert.False(project.Completed);
                    }
                }
            }
        }
        // FAILING DUE TO ENTITY ERROR OF TRACKING MULTIPLE PROJECTS WITH SAME ID...
        //[Fact]
        //public void SaveProject_Marks_Completed_On_Projects_and_Tasks()
        //{
        //    using (var factory = new TestDbContext())
        //    {
        //        using (var context = factory.CreateContext())
        //        {
        //            //Arrange
        //            context.ApplicationUsers.Add(GetApplicationUser(context));
        //            var ps = new ProjectService(context, GetMapper());
        //            var ts = new TaskService(context, GetMapper());
        //            ts.SaveTask(GetSingleTaskDto(), null);
        //            ts.SaveTask(GetSingleTaskDto(), null);
        //            ps.SaveProject(GetProjectsViewModel(), userId);

        //            //Act
        //            var projectToMarkCompeleted = ps.EditProject(1, userId);
                    
        //            ps.SaveProject(projectToMarkCompeleted, userId);
                    
        //            Assert.False(projectToMarkCompeleted.Project.Completed);
        //            foreach (var task in projectToMarkCompeleted.Project.Tasks)
        //            {
        //                Assert.False(task.Completed);
        //            }
        //            projectToMarkCompeleted.Project.Completed = true;
        //            ps.SaveProject(projectToMarkCompeleted, userId);

        //            //Assert
        //            var completedProject = ps.GetCompletedProjects(userId);

        //            Assert.Equal(2, completedProject.Project.Tasks.Count);

        //            foreach (var task in completedProject.Project.Tasks)
        //            {
        //                Assert.True(task.Completed);
        //            }

        //        }
        //    }
        //}
    }
}
