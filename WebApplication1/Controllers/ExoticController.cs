using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ExoticController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("destinyItems");
            var collection = database.GetCollection<Exotic>("exotics");
            
            var test = await collection.FindSync(FilterDefinition<Exotic>.Empty).ToListAsync();

            return View(test);
        }

        public IActionResult Details(string id)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("destinyItems");
            var collection = database.GetCollection<Exotic>("exotics");

            var filter = Builders<Exotic>.Filter.Eq("_id", ObjectId.Parse(id));
            
            var test = collection.FindSync(filter);

            var value = test.SingleOrDefault();

            return View(value);
        }

        public IActionResult Delete(string id)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("destinyItems");
            var collection = database.GetCollection<Exotic>("exotics");
            
            var filter = Builders<Exotic>.Filter.Eq("_id", ObjectId.Parse(id));

            var test = collection.DeleteOne(filter);
            
            return RedirectToAction("Index", "Exotic");
        }
        
        public IActionResult Update(string documentId, Exotic updatedValues)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("destinyItems");
            var collection = database.GetCollection<Exotic>("exotics");

            var builder = Builders<Exotic>.Filter;
            var filter = builder.Eq(x => x._id, ObjectId.Parse(documentId));
            
            var update = Builders<Exotic>.Update
                .Set("thumb", updatedValues.thumb)
                .Set("name", updatedValues.name)
                .Set("isAvailable", updatedValues.isAvailable)
                .Set("weaponType", updatedValues.weaponType)
                .Set("unlockMethod", updatedValues.unlockMethod)
                .Set("unlockMethodDesc", updatedValues.unlockMethodDesc)
                .Set("completionCriteria", updatedValues.completionCriteria)
                .Set("perkDescription", updatedValues.perkDescription)
                .Set("unlockCategory", updatedValues.unlockCategory);
            
            var test = collection.UpdateOne(filter, update, new UpdateOptions());

            return RedirectToAction("Index", "Exotic");
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Exotic exotic)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("destinyItems");
            var collection = database.GetCollection<Exotic>("exotics");

            collection.InsertOne(exotic);

            return RedirectToAction("Index", "Exotic");
        }

        public IActionResult Stats()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("destinyItems");
            var collection = database.GetCollection<Exotic>("exotics");

            var map = new BsonJavaScript("function() { emit( this.weaponType, 1 ); }");
            var reduce = new BsonJavaScript("function(key, values) {return Array.sum(values)}");

            var result = collection.MapReduce<StatsModel>(map, reduce);

            var listResult = result.ToList();
            
            return View(listResult);
        }
    }
}