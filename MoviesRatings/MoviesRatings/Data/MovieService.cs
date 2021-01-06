using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MoviesRatings.Data
{
    public class MovieService : IMoviesService
    {
        private readonly IMongoCollection<Movie> _movies;

        public MovieService(IMongoClient client)
        {
            var database = client.GetDatabase("ProjectDemo");
            _movies = database.GetCollection<Movie>("Movies");
        }
        public async Task<bool> CreateNewMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditMovie(ObjectId Id, Movie movie)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _movies.Find(s => s.Name == "The Avengers").ToListAsync();
        }
            
    }
}
