using Our.Umbraco.Gandalf.Core.Models.DTOs;
using Our.Umbraco.Gandalf.Core.Models.Pocos;
using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;

namespace Our.Umbraco.Gandalf.Core.Extensions
{
    public static class AllowedIpExtensions
	{
		public static AllowedIpDto ToDto(this AllowedIp poco)
		{
			var dto = new AllowedIpDto()
			{
				Id = poco.Id,
				IpAddress = poco.IpAddress,
				Notes = poco.Notes,
				LastUpdated = poco.LastUpdated,
			};

			return dto;
		}


		public static StatusDto ToDto(this KeyValueDto poco)
		{
			bool.TryParse(poco.Value, out var enabled);

			var dto = new StatusDto()
			{
				Enabled = enabled
			};

			return dto;
		}
	}
}
