using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RedSky.FileManagement.Contracts.Entities
{
    public class FileData
    {
        public FileData()
        {
            //Empty constructor for BsonSerializer
        }

        [BsonId]
        public ObjectId MongoId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Uploaded { get; set; }
        public byte[] File { get; set; }
    }
}
