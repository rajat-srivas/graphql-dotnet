using GraphQL.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.Services
{
	public interface ISpeakerRepository
	{
		public Task<List<Speaker>> Speakers { get; }

		public Task<List<Speaker>> GetSpeaker(string id);

		public Task<string> CreateSpeaker(Speaker speakerToCreate);
	}
}
