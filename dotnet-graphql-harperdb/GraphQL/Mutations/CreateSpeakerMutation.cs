using dotnet_graphql_harperdb.Services;
using GraphQL.Data;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.GraphQL.Mutation
{
	public class CreateSpeakerMutation
	{
		ISpeakerRepository _repository;
		public CreateSpeakerMutation(ISpeakerRepository repository)
		{
			_repository = repository;
		}
		public async Task<SpeakerPayload> AddSpeakerAsync(Speaker speaker)
		{
			var speakerToAdd = speaker;
			var id = await _repository.CreateSpeaker(speakerToAdd);
			speakerToAdd.Id = id;
			return new SpeakerPayload(speakerToAdd);
		}
	}
}
