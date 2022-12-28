using dotnet_graphql_harperdb.Context;
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
using HotChocolate.AspNetCore.Authorization;
using dotnet_graphql_harperdb.GraphQL.Mutations;
using dotnet_graphql_harperdb.Helpers;
using GraphQL.Mutations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = Configuration["Jwt:Issuer"],
					ValidAudience = Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = false,
					ValidateIssuerSigningKey = true
				};
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("GeneralUser", policy =>
				{
					policy.RequireClaim("UserRole", "R");
				});
			});


			services.AddControllers();
			services.AddSingleton<IHarperConfiguration, HarperConfigurations>();
			services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
			services.AddSingleton<ISpeakerRepository, SpeakerRepository>();
			services.AddSingleton<AuthHelpers, AuthHelpers>();
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddGraphQLServer()
				.AddAuthorization()
		   .AddQueryType<Query>()
		   .AddMutationType<MutationAggregator>()
			.AddTypeExtension<CreateSpeakerMutation>()
			 .AddTypeExtension<AuthMutation>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGraphQL();
			});
		}
	}
}
