using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DTOs;

namespace ToDoList.Controllers
{
    public class Projects : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetProjects()
        {
            return View("Projects");
        }
        public ActionResult New()
        {
            ProjectDto projectDto = new ProjectDto();


            return View("ProjectForm", projectDto);
        }
    }
}
