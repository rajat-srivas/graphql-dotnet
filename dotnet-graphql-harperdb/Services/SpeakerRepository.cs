using dotnet_graphql_harperdb.Context;
using dotnet_graphql_harperdb.Services;
using GraphQL.Data;
using HarperNetClient;
using HarperNetClient.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Services
{
	public  class SpeakerRepository : ISpeakerRepository
	{
		IRepository<Speaker> _repo;
		private readonly string Table_Name = "speakers";
		public SpeakerRepository(IRepository<Speaker> repo)
		{
			_repo = repo;
		}

		public async Task<List<Speaker>> GetSpeaker(string id)
		{
			return await _repo.GetById(id, Table_Name);
		}

		public Task<List<Speaker>> Speakers
		{
			get
			{
				return GetAllSpeakers();
			}
		}
		private async Task<List<Speaker>> GetAllSpeakers()
		{
			return await _repo.GetAll(Table_Name);
			
		}

		public async Task<string> CreateSpeaker(Speaker speakerToCreate)
		{
			if (string.IsNullOrEmpty(speakerToCreate.Id))
			{
				speakerToCreate.Id = Guid.NewGuid().ToString();
			}
			speakerToCreate?.Books?.ForEach(x =>
			{
				if (string.IsNullOrEmpty(x.Isbn))
				{
					speakerToCreate.Id = Guid.NewGuid().ToString().Replace("-","");
				}

			});
			return await _repo.Create(speakerToCreate, Table_Name);
		}
	}
}
