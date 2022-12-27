using GraphQL.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.Context
{
	public interface IRepository<T> where T : class
	{
		public Task<List<T>> GetAll(string tableName);
		public Task<List<T>> GetById(string id, string tableName);

		public Task<string> Create(T itemToCreate, string tableName);

		public Task<bool> Delete(string id, string tableName);
	}
}
