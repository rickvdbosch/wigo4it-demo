using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Wigo4IT.Common.Entities;

namespace Wigo4IT.API
{
    public static class GetMunicipality
    {
        [FunctionName("GetMunicipality")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "municipalities/{name}")] HttpRequest req,
            [Table("data", "municipalities", "{name}", Connection = "storageConnStr")] MunicipalityEntity entity,
            ILogger log)
        {
            return new OkObjectResult(entity);
        }
    }
}
