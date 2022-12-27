using System.ComponentModel.DataAnnotations;

namespace dotnet_graphql_harperdb.Data
{
	public class UserLogin
	{
		[Required]
		public string Eamil { get; set; }

		[Required]
		public string Password { get; set; }

		public UserLogin()
		{
			Eamil = string.Empty;
			Password = string.Empty;
		}
	}

	public class AuthResponse
	{
		public string ErrorMessage { get; set; }

		public string Token { get; set; }
	}
}
