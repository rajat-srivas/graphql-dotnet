using dotnet_graphql_harperdb.Context;
using dotnet_graphql_harperdb.GraphQL.DataLoaders;
using dotnet_graphql_harperdb.GraphQL.Mutation;
using dotnet_graphql_harperdb.GraphQL.Query;
using dotnet_graphql_harperdb.Services;
using GraphQL.Context;
using GraphQL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace dotnet_graphql_harperdb
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSingleton<IHarperConfiguration, HarperConfigurations>();
			services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
			services.AddSingleton<ISpeakerRepository, SpeakerRepository>();
			services.AddGraphQLServer()
				.AddQueryType<Query>()
				.AddDataLoader<SpeakerByIdDataLoader>()
				.AddMutationType<CreateSpeakerMutation>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGraphQL();
			});
		}
	}
}
