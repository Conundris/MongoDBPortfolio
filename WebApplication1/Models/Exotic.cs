using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    [BsonIgnoreExtraElements]
    //[ModelBinder(BinderType = typeof(ObjectIdBinder))]
    public class Exotic
    {
        [BsonId]
        [MongoObjectID]
        public ObjectId _id { get; set; }
        [BsonElement("thumb")]
        public string thumb { get; set; }
        [BsonElement("name")]
        public string name { get; set; }
        [BsonElement("isAvailable")]
        public bool isAvailable { get; set; }
        [BsonElement("weaponType")]
        public string weaponType { get; set; }
        [BsonElement("unlockMethod")]
        public string unlockMethod { get; set; }
        [BsonElement("unlockMethodDesc")]
        public string unlockMethodDesc { get; set; }
        [BsonElement("completionCriteria")]
        public string completionCriteria { get; set; }
        [BsonElement("perkDescription")]
        public string perkDescription { get; set; }
        [BsonElement("unlockCategory")]
        public string unlockCategory { get; set; }
    }
}