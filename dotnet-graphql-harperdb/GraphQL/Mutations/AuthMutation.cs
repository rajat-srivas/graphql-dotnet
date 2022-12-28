using dotnet_graphql_harperdb.Data;
using dotnet_graphql_harperdb.GraphQL.Schema.SharedSchema;
using dotnet_graphql_harperdb.Helpers;
using dotnet_graphql_harperdb.Services;
using GraphQL.Mutations;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.GraphQL.Mutations
{	
	[ExtendObjectType(Name = "MutationAggregator")]
	public class AuthMutation
	{
		IUserRepository _repository;
		AuthHelpers _authHelper;


		public AuthMutation(IUserRepository repository, AuthHelpers _helper)
		{
			_repository = repository;
			_authHelper = _helper;
		}

		[GraphQLDescription("Authenticate the user")]
		public async Task<AuthResponse> LoginUser(UserLogin userDetails)
		{
			var user = await _repository.CheckUserCred(userDetails.Eamil, userDetails.Password);
			if(user != null && !string.IsNullOrEmpty(user.Email)) {
				string token = _authHelper.GenerateJWT(user);
				return new AuthResponse { Token = token };
			}
			else
			{
				return new AuthResponse { ErrorMessage = "Invalid Email or Password, please try again" };
			}
		}

	}
}
