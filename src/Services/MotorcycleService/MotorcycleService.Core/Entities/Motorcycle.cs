using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MotorcycleService.Core.Entities
{
    public class Motorcycle
    {
        public Motorcycle(
            string id,
            int year,
            string model,
            string plate)
        {
            Id = id;
            Year = year;
            Model = model;
            Plate = plate.ToUpper();
        }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; internal set; }
        public int Year { get; internal set; }
        public string Model { get; internal set; }
        public string Plate { get; internal set; }

        public void UpdatePlate(string plate) => Plate = plate.ToUpper();
    }
}
