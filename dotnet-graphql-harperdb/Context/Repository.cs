using GraphQL.Context;
using HarperNetClient.models;
using HarperNetClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Data;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;

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
	}
}