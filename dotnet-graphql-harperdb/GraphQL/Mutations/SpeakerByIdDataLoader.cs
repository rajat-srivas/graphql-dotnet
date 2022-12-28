using dotnet_graphql_harperdb.Services;
using GraphQL.Data;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_graphql_harperdb.GraphQL.Mutations
{
    public class SpeakerByIdDataLoader : HotChocolate.DataLoader.BatchDataLoader<string, Speaker>
    {
        ISpeakerRepository _repository;
        public SpeakerByIdDataLoader(ISpeakerRepository repository, IBatchScheduler _batchScheduler) : base(_batchScheduler)
        {
            _repository = repository;
        }

        protected override async Task<IReadOnlyDictionary<string, Speaker>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var speaker = await _repository.GetSpeakerById(keys[0]);
            return speaker.ToDictionary(x => x.Id);
        }
    }
}
