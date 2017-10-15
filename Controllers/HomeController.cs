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
            ViewBag.Now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
            return View();
        }
        [HttpGet]
        [Route("quotes")]
        public IActionResult quotes()
        {
			string query = "SELECT * FROM  QuotesTable";
			var result = ctx.Query(query);
			ViewBag.allQuotes = result;
			return View();
		}

        [HttpPost]
        [Route("addQuote")]
        public IActionResult addQuote(string UserName, string Quote)
        {
            UserName.Replace("'","_");
			Quote.Replace("'", "`");
            string time=@DateTime.Now.ToString("yyyy-MM-dd, hh:mm:ss tt");
			string query = $"INSERT INTO QuotesTable(UserName, Quote, DateAdded) values('{@UserName}', '{@Quote}' , '{@time}' )";
			ctx.Execute(query);
			return RedirectToAction("Index");
        }
    }
}
