using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using DbConnection;
using QuotingDojo.Models;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        private DbConnector _dbConnector;
        public HomeController()
        {
            _dbConnector = new DbConnector();
        }
        

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        // POST: form post route /quotes
        [HttpPost("quotes")]
        public IActionResult CreateQuote(string name, string quote)
        {
            // Making query to insert into the table name(Table Fields) VALUES using selecter '$' (from the Index.cshtml Field names)
            var query = $"INSERT INTO quotes(name, quote, createdat) VALUES  ('{name}', '{quote}', NOW())";
            // Execute the query
            _dbConnector.Execute(query);

            // Consolelog if quote went through
            System.Console.WriteLine("\n\n\nQuote received!");

            // Redirect to getQuotes action route > View Quotes
            return RedirectToAction("getQuotes");
        }

        // Get all quotes
        [HttpGet("quotes")]
        public IActionResult getQuotes()
        {
            //query to SELECT ALL FROM table name ORDER BY createdat table field DESC(latest to oldest)
            var query = "SELECT * FROM quotes ORDER BY createdat DESC";
            // Putting query into allQuotes variable
            var allQuotes = _dbConnector.Query(query);
            // allQuotes query into ViewBag
            ViewBag.allQuotes = allQuotes;
            // View Quotes.cshtml page
            return View("Quotes");
        }
    }

}
