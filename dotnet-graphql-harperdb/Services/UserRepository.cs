using dotnet_graphql_harperdb.Context;
using dotnet_graphql_harperdb.Data;
using GraphQL.Data;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.Services
{
	public class UserRepository : IUserRepository
	{
		IRepository<User> _repo;
		private readonly string Table_Name = "users";
		public UserRepository(IRepository<User> repo)
		{
			_repo = repo;
		}
		public async Task<User> CheckUserCred(string email, string password)
		{
			return await _repo.CheckUserCred(email, password, Table_Name);

		}

		public async Task<string> CreateUser(User user)
		{
			return await _repo.Create(user, Table_Name);
		}
	}
}
