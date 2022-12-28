﻿using dotnet_graphql_harperdb.Context;
using dotnet_graphql_harperdb.GraphQL.Mutations;
using dotnet_graphql_harperdb.Services;
using GraphQL.Data;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
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

		[Authorize(Policy = "GeneralUser")]
        [UsePaging(IncludeTotalCount =true)]
        [UseFiltering]
        [UseSorting]
		[GraphQLDescription("Get all speakers")]

		public async Task<IQueryable<Speaker>> GetSpeakers()
        {
            var data = await _repository.Speakers;
            return data.AsQueryable();
        }

		[Authorize(Policy = "GeneralUser")]
		[GraphQLDescription("Get speaker by Id")]

        // Using a data loader helps in caching so for same id it will alays return the same data
        public async Task<Speaker> GetSpeakerById(string id, SpeakerByIdDataLoader loader, CancellationToken token)
        {
            var data = await loader.LoadAsync(id, token);
            return data;
        }

		[Authorize(Policy = "GeneralUser")]
		[GraphQLDescription("Get speaker by Name")]

		public async Task<List<Speaker>> GetSpeakerByName(string name)
		{
			var data = await _repository.GetSpeakerByName(name);
			return data;
		}

	}
}
