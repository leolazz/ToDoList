using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.DTOs
{
    public class TaskDto
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public TaskDetailsDto Details { get; set; }
        public TaskOutcomesDto Outcomes { get; set; }
        public TaskQualifiersDto Qualifiers { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Deadline")]
        public DateTime EndDate { get; set; }
        public DateTime ETA { get; set; }
        [Display(Name = "Task Status")]
        public bool Completed { get; set; }

    }
}
