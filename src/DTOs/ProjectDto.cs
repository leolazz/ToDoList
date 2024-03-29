﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.DTOs
{
    public class ProjectDto
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Project Project { get; set; }
        public List<DTOs.TaskDto> Tasks { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Deadline")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Task Status")]
        public bool Completed { get; set; }
    }
}
