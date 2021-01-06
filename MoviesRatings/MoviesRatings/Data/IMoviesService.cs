using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRatings.Data
{
    public interface IMoviesService
    {
        Task<List<Movie>> GetMovies();
        Task<bool> CreateNewMovie(Movie movie);
        Task<bool> EditMovie(ObjectId Id, Movie movie);        
    }
}
