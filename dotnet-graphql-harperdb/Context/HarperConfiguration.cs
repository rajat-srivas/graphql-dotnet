using HarperNetClient.models;

namespace GraphQL.Context
{
	public class HarperConfigurations : IHarperConfiguration
	{
		public HarperConfigurations()
		{
		}
		public HarperDbConfiguration GetHarperConfigurations()
		{
			return new HarperDbConfiguration()
			{
				InstanceUrl = "https://graphql-dotnet-coredemo.harperdbcloud.com",
				AuthToken = "=",
				Schema = "graphql"
			};
		}
	}
}
