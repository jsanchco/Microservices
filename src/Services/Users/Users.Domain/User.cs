using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Users.Domain
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string DNI { get; set; }
        public DateTime Birthdate { get; set; }

        public List<Residence> Residences { get; set; }
    }
}
