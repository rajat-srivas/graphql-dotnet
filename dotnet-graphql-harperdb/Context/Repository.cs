using GraphQL.Context;
using HarperNetClient.models;
using HarperNetClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Data;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using dotnet_graphql_harperdb.Data;

namespace dotnet_graphql_harperdb.Context
{
	public class Repository<T> : IRepository<T> where T : class
	{
		IHarperConfiguration _config;
		HarperDbConfiguration _dbConfig;
		IHarperClientAsync _client;

		public Repository(IHarperConfiguration config)
		{
			_config = config;
			_dbConfig = _config.GetHarperConfigurations();
		}

		public async Task<List<T>> GetAll(string tableName)
		{
			_client = new HarperClientAsync(_dbConfig, tableName);
			var response = await _client.ExecuteQueryAsync($"SELECT * FROM {_dbConfig.Schema}.{tableName}");
			return JsonConvert.DeserializeObject<List<T>>(response.Content);
		}

		public async Task<List<T>> GetById(string id, string tableName)
		{
			_client = new HarperClientAsync(_dbConfig, tableName);
			var response = await _client.GetByIdAsync(id);
			var speakers = JsonConvert.DeserializeObject<List<T>>(response.Content);
			return speakers;
		}

		public async Task<string> Create(T itemToCreate, string tableName)
		{
			_client = new HarperClientAsync(_dbConfig, tableName);
			var response = await _client.CreateRecordAsync<T>(itemToCreate);
			var insertedCommentId = JsonConvert.DeserializeObject<Content>(response.Content).Inserted_Hashes[0];
			return insertedCommentId;
		}

		public Task<bool> Delete(string id, string tableName)
		{
			throw new System.NotImplementedException();
		}

		public async Task<T> CheckUserCred(string email, string password, string tableName)
		{
			_client = new HarperClientAsync(_dbConfig, tableName);
			var response = await _client.ExecuteQueryAsync($"SELECT * FROM {_dbConfig.Schema}.{tableName} T WHERE T.Email =  \"{email}\" AND T.Password =  \"{password}\"");
			var result = JsonConvert.DeserializeObject<List<T>>(response.Content);
			if(result != null && result.Count > 0)
			{
				return result[0];
			}
			return null;
		}

		public async Task<List<T>> GetByName(string name, string table_Name)
		{
			_client = new HarperClientAsync(_dbConfig, table_Name);
			var response = await _client.ExecuteQueryAsync($"SELECT * FROM {_dbConfig.Schema}.{table_Name} T WHERE T.Name =  \"{name}\"");
			var data = JsonConvert.DeserializeObject<List<T>>(response.Content);
			return data;
		}

		public async Task<bool> Update(T itemToUpdate, string tableName)
		{
			_client = new HarperClientAsync(_dbConfig, tableName);
			var result = await _client.UpdateRecordAsync<T>(itemToUpdate);
			return result.IsSuccessful;
		}
	}
}