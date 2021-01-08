using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace MoviesRatings.Data
{
    public class ActorService : IActorService
    {
        private readonly IMongoCollection<Actor> _actor;

        public ActorService(IMongoClient client)
        {
            var database = client.GetDatabase("ProjectDemo");
            _actor = database.GetCollection<Actor>("Actors");
        }
        public async Task<bool> CreateNewActor(Actor actor)
        {

            try
            {
                await _actor.InsertOneAsync(actor);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> DeleteActor(ObjectId id)
        {
            //Get the Actor object id
            try
            {
                await _actor.DeleteOneAsync(actor => actor.Id == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditActor(ObjectId Id, Actor actor)
        {
            try
            {
                var currentActor = Builders<Actor>.Filter.Eq(e => e.Id, Id);
                var update = Builders<Actor>.Update.Set(e => e.FirstName, actor.FirstName)
                    .Set(e => e.LastName, actor.LastName)
                    .Set(e => e.Gender, actor.Gender);

                await _actor.UpdateOneAsync(currentActor, update);
                return true;
            }
            catch
            {
                return false;
            }
                
        }

        public async Task<Actor> GetActor(ObjectId id)
        {
            return  await _actor.Find(e => e.Id == id).SingleAsync();
            
        }

        public async Task<List<Actor>> GetActors()
        {
            return await _actor.Find(e => true).ToListAsync();
        }
    }
}
