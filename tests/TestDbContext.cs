using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using ToDoList.Models;

namespace ToDoList.Tests
{
    public class SampleDbContextFactory : IDisposable
    {
        private DbConnection _connection;
        private DbContextOptions<TestSQLiteDBContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<TestSQLiteDBContext>()
                .UseSqlite(_connection).Options;
        }
        public TestSQLiteDBContext CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                var options = CreateOptions();
                using (var context = new TestSQLiteDBContext(options))
                {
                    context.Database.EnsureCreated();
                }
            }
            return new TestSQLiteDBContext(CreateOptions());
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public class TestSQLiteDBContext : IdentityDbContext<ApplicationUser>
        {
            public TestSQLiteDBContext(DbContextOptions options)
            : base(options)
            {
            }
            public TestSQLiteDBContext()
            {
            }
            public DbSet<Task> Tasks { get; set; }
            public DbSet<TaskDetails> Details { get; set; }
            public DbSet<TaskOutcomes> Outcomes { get; set; }
            public DbSet<TaskQualifiers> Qualifiers { get; set; }
            public DbSet<ApplicationUser> ApplicationUsers { get; set; }
            public DbSet<Project> Projects { get; set; }

        }
    }
}

