using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos.Table;

using Wigo4IT.Common.Entities;

namespace Wigo4IT.API
{
	public static class GetMunicipalities
    {
        [FunctionName(nameof(GetMunicipalities))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "municipalities")] HttpRequest req,
            [Table("data", Connection = "storageConnStr")] CloudTable cloudTable,
            ILogger log)
        {
            var entities = await GetEntitiesFromTableByPartitionKey<MunicipalityEntity>(cloudTable, "municipalities");

            return new OkObjectResult(entities.Select(m => m.Name).ToList());
        }

        private static async Task<IEnumerable<T>> GetEntitiesFromTableByPartitionKey<T>(CloudTable table, string partitionKey) where T : ITableEntity, new()
        {
            TableQuerySegment<T> querySegment = null;
            var entities = new List<T>();
            var query = new TableQuery<T>().Where(TableQuery.GenerateFilterCondition(nameof(ITableEntity.PartitionKey), QueryComparisons.Equal, partitionKey));

            do
            {
                querySegment = await table.ExecuteQuerySegmentedAsync(query, querySegment?.ContinuationToken);
                entities.AddRange(querySegment.Results);
            } while (querySegment.ContinuationToken != null);

            return entities;
        }
    }
}
