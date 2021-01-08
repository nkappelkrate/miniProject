using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MoviesRatings.Data
{
    public class Actor
    {
        public ObjectId Id { get; set; } 
        //public ObjectId CastId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
}
