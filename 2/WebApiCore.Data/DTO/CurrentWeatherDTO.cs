using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiCore.Data.DTO
{
    public class CurrentWeatherDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Status { get; set; }
        public float Temp { get; set; }
        public float MinTemp { get; set; }
        public float MaxTemp { get; set; }
    }
}