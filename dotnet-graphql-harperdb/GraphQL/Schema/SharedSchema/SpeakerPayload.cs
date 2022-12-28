using GraphQL.Data;

namespace dotnet_graphql_harperdb.GraphQL.Schema.SharedSchema
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
