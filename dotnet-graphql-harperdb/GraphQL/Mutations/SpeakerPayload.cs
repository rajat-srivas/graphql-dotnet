using GraphQL.Data;

namespace dotnet_graphql_harperdb.GraphQL.Mutation
{
	public class SpeakerPayload
	{
		public Speaker Speaker { get; }
		public SpeakerPayload(Speaker speaker)
		{
			Speaker = speaker;
		}
	}
}
