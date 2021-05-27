using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DTOs;
using ToDoList.Models;
using Task = ToDoList.Models.Task;

namespace ToDoList.ViewModels
{
    public class TaskViewModel
    {
        public int? Id { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        public string Title { get; set; }
        public TaskDetails Details { get; set; }
        public TaskOutcomes Outcomes { get; set; }
        public TaskQualifiers Qualifiers { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Deadline")]
        public DateTime EndDate { get; set; }
        public DateTime ETA { get; set; }
        [Display(Name = "Task Status")]
        public bool Completed { get; set; }

        public TaskViewModel()
        {
            Id = 0;
        }
        public TaskViewModel(Task task)
        {
            Id = task.Id;
            UserId = task.UserId;
            Title = task.Title;
            Details = task.Details;


        }

    }
}
