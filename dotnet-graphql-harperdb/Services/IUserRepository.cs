using dotnet_graphql_harperdb.Data;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.Services
{
	public interface IUserRepository
	{
		Task<string> CreateUser(User user);

		Task<User> CheckUserCred(string email, string password);
	}
}
