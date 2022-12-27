using dotnet_graphql_harperdb.Data;
using dotnet_graphql_harperdb.Services;
using HotChocolate;
using System.Threading.Tasks;

namespace GraphQL.Mutations
{
	public class MutationAggregator
	{
		IUserRepository _repository;
		public MutationAggregator(IUserRepository repo)
		{
			_repository= repo;
		}

		[GraphQLDescription("Register a new user")]
		public async Task<string> UserRegistration(User registrationData)
		{
			if (registrationData != null && string.IsNullOrEmpty(registrationData.UserRole)) { registrationData.UserRole = "R"; }
			string registeredUserId = await _repository.CreateUser(registrationData);

			if (!string.IsNullOrEmpty(registeredUserId))
			{
				return registeredUserId;
			}
			else
			{
				return "Something went wrong, User Registration Failed";
			}
		}
	}
}
