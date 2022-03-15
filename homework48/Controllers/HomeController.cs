using homework48.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Homework48.Data;

namespace homework48.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NorthWnd;Integrated Security=true;";
        public IActionResult Index()
        {
            var mgr = new Manager(_connectionString);
            var people = mgr.GetPeople();
            var model = new IndexViewModel();
            string message = (string)TempData["Message"];
            if (!String.IsNullOrEmpty(message))
            {
                model.Message = message;
            }
            model.People = people;
            return View(model);
        }
        public IActionResult ShowAddManyPeople()
        {
            return View();
        }
        public IActionResult AddManyPeople(List<Person> people)
        {
            var mgr = new Manager(_connectionString);
            foreach(Person person in people)
            {
                mgr.AddPeople(person);
            }
            string PersonOrPeople = (people.Count == 1) ? "person" : "people";
            TempData["Message"] = $"{people.Count} {PersonOrPeople} added successfully";
            return Redirect("/Home/Index");
        }
       
    }
}
