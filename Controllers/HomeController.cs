using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuotingDojo.Connections;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        private MySqlConnector ctx;
        public HomeController()
        {
            ctx = new MySqlConnector();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            string query = "SELECT * FROM  QuotesTable";
            var result = ctx.Query(query);
            ViewBag.allQuotes = result;
            return View();
        }
    }
}
