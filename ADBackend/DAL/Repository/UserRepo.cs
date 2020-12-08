using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ADBackend.DAL.Interfaces;
using Dapper;
using Google.Cloud.Datastore.V1;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;

namespace ADBackend.DAL.Repository
{
    public class UserRepo : BaseRepository, IUserRepo
    {
        private readonly DatastoreDb _db;

        public UserRepo(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings.Value)
        {
            _db = DatastoreDb.Create(connectionStrings.Value.DataStore);
        }



        public bool IsUserAdmin(string token)
        {
            Query query = new Query("Admin")
            {
                Filter = Filter.And(Filter.Equal("UID", token))
            };
            var results = _db.RunQuery(query).Entities;
            bool result = results.Any();
            return result;
        }
    }
}

