using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MoviesRatings.Data
{
    public interface IActorService
    {
        Task<List<Actor>> GetActors();
        Task<Actor> GetActor(ObjectId id);
        Task<bool> CreateNewActor(Actor actor);
        Task<bool> EditActor(ObjectId Id, Actor actor);
        Task<bool> DeleteActor(ObjectId Id);
    }
}
