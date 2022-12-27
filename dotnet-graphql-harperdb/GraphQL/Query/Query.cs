using dotnet_graphql_harperdb.Context;
using dotnet_graphql_harperdb.GraphQL.DataLoaders;
using dotnet_graphql_harperdb.Services;
using GraphQL.Data;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.GraphQL.Query
{
    public class Query
    {
        ISpeakerRepository _repository;
        public Query(ISpeakerRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IQueryable<Speaker>> GetSpeakers()
        {
            var data = await _repository.Speakers;
            return data.AsQueryable();
        }

        public async Task<Speaker> GetSpeaker(string id, SpeakerByIdDataLoader loader, CancellationToken token)
        {
            var data = await loader.LoadAsync(id, token);
            return data;
        }
    }
}
