using AutoMapper;
using dotnet_graphql_harperdb.Data;
using dotnet_graphql_harperdb.GraphQL.Schema.MutationSchema;
using GraphQL.Data;

namespace dotnet_graphql_harperdb.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<SpeakerType, Speaker>().ReverseMap();
			CreateMap<BookType, Book>().ReverseMap();
			CreateMap<UserType, User>().ReverseMap();
		}
	}
}
