﻿using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonElement("Name")]
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Summary { get; private set; }
        public string Description { get; private set; }
        public string ImageFile { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, string category, string summary, string description, string imageFile, decimal price)
        {
            Name = name;
            Category = category;
            Summary = summary;
            Description = description;
            ImageFile = imageFile;
            Price = price;
        }
    }
}
