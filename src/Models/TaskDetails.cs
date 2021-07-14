using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class TaskDetails
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Details { get; set; }
    }
}
