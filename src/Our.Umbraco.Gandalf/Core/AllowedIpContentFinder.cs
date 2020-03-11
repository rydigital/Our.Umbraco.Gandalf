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
			var status = _allowedIpService.GetStatus();

			if (status.Value != true.ToString())
			{
				return false;
			}

			var ip = request.UmbracoContext.HttpContext.Request.UserHostAddress;
			var item = _allowedIpService.GetByIpAddress(ip);

			if (item != null)
			{
				request.SetRedirect("/ip-not-allowed");

				return true;
			}

			return false;
		}
	}
}
