using Our.Umbraco.Gandalf.Core.Models.DTOs;
using Our.Umbraco.Gandalf.Core.Models.Pocos;

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
	}
}
