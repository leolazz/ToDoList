using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;

namespace ToDoList.Data
{
    
        public class SQLiteDBContext : IdentityDbContext<ApplicationUser>
        {
          //  public DbSet<Project> Projects { get; set; }
            public DbSet<Task> Tasks { get; set; }
            public DbSet<TaskDetails> Details { get; set; }
            public DbSet<TaskOutcomes> Outcomes { get; set; }
            public DbSet<TaskQualifiers> Qualifiers { get; set; }
            public DbSet<ApplicationUser> ApplicationUsers { get; set; }
            public DbSet<Project> Projects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) 
                => options.UseSqlite("Data Source=DB/sqlitetodolist.db");
            public SQLiteDBContext(DbContextOptions<SQLiteDBContext> options)
            : base(options)
            {
            }

        }
        

    }
