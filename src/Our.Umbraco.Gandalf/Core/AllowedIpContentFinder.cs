using Umbraco.Web.Routing;
using Our.Umbraco.Gandalf.Core.Services;

namespace Our.Umbraco.Gandalf.Core
{
	public class AllowedIpContentFinder : IContentFinder
	{
		private readonly IAllowedIpService _allowedIpService;

		public AllowedIpContentFinder(IAllowedIpService allowedIpService)
		{
			_allowedIpService = allowedIpService;
		}

		public bool TryFindContent(PublishedRequest request)
		{
			var ip = request.UmbracoContext.HttpContext.Request.UserHostAddress;
			var status = _allowedIpService.GetStatus();

			var item = _allowedIpService.GetByIpAddress(ip);
			bool.TryParse(status.Value, out bool boolValue); 

			if (item != null && boolValue)
			{
				request.SetRedirect("/ip-not-allowed");

				return true;
			}

			return false;
		}
	}
}
