using GraphQL.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.Services
{
	public interface ISpeakerRepository
	{
		public Task<List<Speaker>> Speakers { get; }

		public Task<List<Speaker>> GetSpeakerById(string id);

		public Task<string> CreateSpeaker(Speaker speakerToCreate);
		public Task<string> UpdateSpeaker(Speaker speakerToCreate);
		Task<List<Speaker>> GetSpeakerByName(string name);
	}
}
