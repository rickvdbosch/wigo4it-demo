using Microsoft.Azure.Cosmos.Table;

namespace Wigo4IT.Common.Entities
{
	public class MunicipalityEntity : TableEntity
	{
		public MunicipalityEntity()
		{
			PartitionKey = "municipalities";
		}

		[IgnoreProperty]
		public string Name => RowKey;

		public string Province { get; set; }

		public int Inhabitants { get; set; }

		public string Nickname { get; set; }
	}
}
