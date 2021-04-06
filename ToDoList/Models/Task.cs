using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class Task
    {
        [Required]
        public byte Id { get; set; }
        public string Title { get; set; }
        public TaskDetails Details { get; set; }
        public TaskOutcomes Outcomes { get; set; }
        public TaskQualifiers Qualifiers { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Deadline")]
        public DateTime EndDate { get; set; }
        public DateTime ETA { get; set; }
    }
}
