using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class TaskOutcomes
    {
        [Required] 
        public byte Id { get; set; }
        public string Outcomes { get; set; }
    }
}
