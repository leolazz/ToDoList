using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Tests
{
    public class TestDbContext : IDisposable
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

        public class TestSQLiteDBContext : ToDoListDbContext
        {
            public TestSQLiteDBContext(DbContextOptions options)
            : base(options)
            {
            }

        }
    }
}

