using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ToDoList.Models;
using ToDoList.Models.ViewModels;

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
            var todoListViewModel = GetAllTodos();
            return View(todoListViewModel);
        }

        public RedirectResult Insert(TodoItem todo)
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

            return Redirect("http://localhost:56279/");
        }

        public TodoViewModel GetAllTodos()
        {
            var todoList = new List<TodoItem>();
            using var con = new SqliteConnection("Data Source=ToDo.db");
            using var tableCmd = con.CreateCommand();
            con.Open();
            tableCmd.CommandText = "SELECT * FROM todo";

            using var reader = tableCmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    todoList.Add(
                        new TodoItem
                        {
                            Id = reader.GetInt32(0),
                            TaskName = reader.GetString(1)
                        });
                }
            }

            return new TodoViewModel
            {
                TodoList = todoList
            };
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            using var con = new SqliteConnection("Data Source=ToDo.db");
            using var tableCmd = con.CreateCommand();
            con.Open();
            tableCmd.CommandText = $"DELETE from todo WHERE Id = '{id}'";
            tableCmd.ExecuteNonQuery();
            return Json(new { });
        }
    }
}
