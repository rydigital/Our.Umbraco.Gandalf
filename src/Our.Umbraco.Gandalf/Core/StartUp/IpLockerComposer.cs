using Umbraco.Web;
using Umbraco.Core.Composing;
using Our.Umbraco.Gandalf.Core.Repositories;
using Our.Umbraco.Gandalf.Core.Services;
using System.Configuration;
using System.ComponentModel;
using Umbraco.Core;
using Our.Umbraco.Gandalf.Core.Services.CachedProxies;

namespace Our.Umbraco.Gandalf.Core.StartUp
{
	public class GandalfComposer : IUserComposer
	{

		public void Compose(Composition composition)
		{
			if (ConfigurationManager.AppSettings["AllowedIpServiceCache:Enabled"] == "true" || ConfigurationManager.AppSettings["AllowedIpServiceCache:Enabled"] == null)
			{
				composition.Register<AllowedIpService, AllowedIpService>();
				composition.Register<IAllowedIpService, AllowedIpServiceCachedProxy>();
			}
			else
			{
				composition.Register<IAllowedIpService, AllowedIpService>();
			}
			composition.Register(typeof(IRepository), typeof(AllowedIpRepository));

			composition.ContentFinders().Insert<NotAllowedLastChangeContentFinder>(0);
			composition.ContentFinders().Insert<AllowedIpContentFinder>(1);

		}
	}
}
