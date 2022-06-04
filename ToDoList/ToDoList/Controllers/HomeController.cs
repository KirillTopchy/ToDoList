using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public void Insert(TodoItem todo)
        {
            using var con =
                   new SqliteConnection("Data Source=ToDo.db");
            using var tableCmd = con.CreateCommand();
            con.Open();
            tableCmd.CommandText = $"INSERT INTO todo (Name) VALUES ('{todo.TaskName}')";
            try
            {
                tableCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
