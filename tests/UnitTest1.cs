using System;
using Xunit;
using ToDoList.Services;
using ToDoList.Models;
using System.Linq;

namespace ToDoList.Tests
{
    public class TaskServicesTests
    {
        [Fact]
        public void NewTask_Returns_New_TaskDto()
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
                    Assert.Equal(0, count);

                    var t = context.Tasks.FirstOrDefault(task => task.Title == "test");
                    Assert.NotNull(t);
                }
            }
        }
    }
}
