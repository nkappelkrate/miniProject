using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRatings.Data
{
    public class Movie
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public double Year { get; set; }
        public string Type { get; set; }
    }
}
