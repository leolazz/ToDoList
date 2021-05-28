using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class Project
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
