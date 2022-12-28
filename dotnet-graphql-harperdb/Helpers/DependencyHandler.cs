using dotnet_graphql_harperdb.Context;
using dotnet_graphql_harperdb.GraphQL.Mutation;
using dotnet_graphql_harperdb.GraphQL.Mutations;
using dotnet_graphql_harperdb.GraphQL.Query;
using dotnet_graphql_harperdb.Helpers.Validators;
using dotnet_graphql_harperdb.Services;
using FluentValidation.AspNetCore;
using GraphQL.Context;
using GraphQL.Mutations;
using GraphQL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace dotnet_graphql_harperdb.Helpers
{
	public static class DependencyHandler
	{
		public static void InjectGraphQLDependencies(this IServiceCollection services)
		{
			services.AddGraphQLServer()
					.AddAuthorization()
					.AddQueryType<Query>()
					.AddMutationType<MutationAggregator>()
					.AddTypeExtension<SpeakerMutations>()
					.AddTypeExtension<AuthMutation>()
					.AddFiltering()
					.AddSorting();
		}

		public static void InjectServices(this IServiceCollection services)
		{
			services.AddSingleton<IHarperConfiguration, HarperConfigurations>();
			services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
			services.AddSingleton<ISpeakerRepository, SpeakerRepository>();
			services.AddSingleton<AuthHelpers, AuthHelpers>();
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddFluentValidation();
			services.AddTransient<SpeakerTypeValidator>();
		}
	}
}
