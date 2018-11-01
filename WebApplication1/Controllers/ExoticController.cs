using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ExoticController : Controller
    {
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

        public void edit(ObjectId objectID)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("destinyItems");
            var collection = database.GetCollection<Exotic>("destiny");

            var test = collection.FindSync(exotic => exotic._id == objectID);

            var value = test.SingleOrDefault();
        }

        public void delete(ObjectId objectId)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("destinyItems");
            var collection = database.GetCollection<Exotic>("destiny");

            var test = collection.DeleteOne(exotic => exotic._id == objectId);
        }
    }
}