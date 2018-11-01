using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<string> test()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("destinyItems");
            var collection = database.GetCollection<Exotic>("destiny");
            
            //return collection.Find(FilterDefinition<Exotic>.Empty).ToJson();
            //return collection.CountDocuments(FilterDefinition<Exotic>.Empty).ToString();
            var test = await collection.FindSync(FilterDefinition<Exotic>.Empty).ToListAsync();

            return test.ToJson();
        }
        
    }
}