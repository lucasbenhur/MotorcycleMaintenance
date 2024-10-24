﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeliveryManService.Core.Entities
{
    public class DeliveryMan
    {
        public DeliveryMan(
            string id,
            string name,
            string cnpj,
            DateTime birthDate,
            string cnhNumber,
            string cnhType,
            string cnhImage)
        {
            Id = id;
            Name = name;
            Cnpj = cnpj;
            BirthDate = birthDate;
            CnhNumber = cnhNumber;
            CnhType = cnhType.ToUpper();
            CnhImage = cnhImage;
        }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public string Cnpj { get; internal set; }
        public DateTime BirthDate { get; internal set; }
        public string CnhNumber { get; internal set; }
        public string CnhType { get; internal set; }
        public string CnhImage { get; internal set; }

        public void UpdateCnhImage(string cnhImage) => CnhImage = cnhImage;
    }
}
