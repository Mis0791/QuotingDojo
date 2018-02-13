using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotingDojo.Models;
using DbConnection;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        private DbConnector cnx;

        public HomeController() {
            cnx = new DbConnector();
        }
        // Get home 
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/quotes/add")]
        public IActionResult AddQuote(string name, string quote)
        {
            // ViewBag.name = name;
            // ViewBag.quote = quote;
            string query = $"INSERT INTO quotes (name, quote, created_at) VALUES ('{name}', '{quote}', NOW())";
            DbConnector.Execute(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/quotes")]
        public IActionResult ShowQuotes()
        {
            string query = "SELECT * FROM quotes ORDER BY created_at DESC";
            var allQuotes = DbConnector.Query(query);
            ViewBag.allQuotes = allQuotes;
            return View("Quote");
        }
    }
}
