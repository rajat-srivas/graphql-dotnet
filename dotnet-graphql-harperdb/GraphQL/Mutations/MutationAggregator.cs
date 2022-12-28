using AutoMapper;
using dotnet_graphql_harperdb.Data;
using dotnet_graphql_harperdb.GraphQL.Schema.MutationSchema;
using dotnet_graphql_harperdb.Services;
using HotChocolate;
using System.Threading.Tasks;

namespace GraphQL.Mutations
{
	public class MutationAggregator
	{
		IUserRepository _repository;
		IMapper _mapper;
		public MutationAggregator(IUserRepository repo, IMapper mapper)
		{
			_repository= repo;
			_mapper= mapper;
		}

		[GraphQLDescription("Register a new user")]
		public async Task<string> UserRegistration(UserType registrationData)
		{
			var user = _mapper.Map<User>(registrationData);
			if (registrationData != null && string.IsNullOrEmpty(user.UserRole)) { user.UserRole = "R"; }
			string registeredUserId = await _repository.CreateUser(user);

			if (!string.IsNullOrEmpty(registeredUserId))
			{
				return registeredUserId;
			}
			else
			{
				throw new GraphQLException(new Error("Something went wrong!", "User_Registration_Failed"));
			}
		}
	}
}
