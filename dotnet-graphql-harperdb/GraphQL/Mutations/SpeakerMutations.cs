using AutoMapper;
using dotnet_graphql_harperdb.GraphQL.Schema.MutationSchema;
using dotnet_graphql_harperdb.GraphQL.Schema.SharedSchema;
using dotnet_graphql_harperdb.Helpers.Validators;
using dotnet_graphql_harperdb.Services;
using GraphQL.Data;
using GraphQL.Mutations;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.GraphQL.Mutation
{
	[ExtendObjectType(Name = "MutationAggregator")]

	public class SpeakerMutations
	{
		ISpeakerRepository _repository;
		private IMapper _mapper;
		SpeakerTypeValidator _speakerTypeValidator;
		public SpeakerMutations(ISpeakerRepository repository, IMapper mapper, SpeakerTypeValidator validator)
		{
			_repository = repository;
			_mapper = mapper;
			_speakerTypeValidator = validator;
		}
		
		[Authorize(Policy = "GeneralUser")]
		[GraphQLDescription("Create new speaker with books")]

		public async Task<SpeakerPayload> CreateSpeaker(SpeakerType speaker)
		{
			var validationResult = _speakerTypeValidator.Validate(speaker);
			if (!validationResult.IsValid)
			{
				throw new GraphQLException(new Error(string.Join(", ", validationResult.Errors.Select(x=> x.ErrorMessage))));
			}

			var speakerToAdd = _mapper.Map<Speaker>(speaker);
			var id = await _repository.CreateSpeaker(speakerToAdd);
			speakerToAdd.Id = id;
			return new SpeakerPayload(speakerToAdd);
		}


		[Authorize(Policy = "GeneralUser")]
		[GraphQLDescription("Update Speaker")]
		public async Task<SpeakerPayload> UpdateSpeaker(string id, SpeakerType speaker)
		{
			var validationResult = _speakerTypeValidator.Validate(speaker);
			if (!validationResult.IsValid)
			{
				throw new GraphQLException(new Error(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage))));
			}
			var speakerToUpdate = _mapper.Map<Speaker>(speaker);
			speakerToUpdate.Id = id;
			var result = await _repository.UpdateSpeaker(speakerToUpdate);
			if (!string.IsNullOrEmpty(result))
			{
				return new SpeakerPayload(speakerToUpdate);
			}
			else
			{
				throw new GraphQLException(new Error("Something went wrong!", $"Update failed for {id}"));
			}
		}
	}
}
