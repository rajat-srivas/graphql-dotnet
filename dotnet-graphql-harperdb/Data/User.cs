using Newtonsoft.Json;

namespace dotnet_graphql_harperdb.Data
{
	public class User
	{
		[JsonProperty(PropertyName ="userId")]
		public string UserId { get; set; }
		public string FirstName { get; set; } 
		public string LastName { get; set; }
		public string Email { get; set; } 
		public string Password { get; set; } 
		public string Gender { get; set; }

		public string UserRole { get; set; }
	}
}
