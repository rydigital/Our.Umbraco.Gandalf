using Umbraco.Web.Routing;

namespace Our.Umbraco.Gandalf.Core
{
	public class NotAllowedLastChangeContentFinder : IContentFinder
	{
		public NotAllowedLastChangeContentFinder()
		{
		}

		public bool TryFindContent(PublishedRequest request)
		{
			var path = request.UmbracoContext.HttpContext.Request.Url.AbsolutePath;

			// TODO: actually handle this in a nice way!
			if (path.Contains("/ip-not-allowed"))
			{
				request.UmbracoContext.HttpContext.Response.StatusCode = 403;
				request.UmbracoContext.HttpContext.Response.Write("Access Denied");

				return true;
			}

			return false;
		}
	}
}
