using Umbraco.Web;
using Umbraco.Core.Composing;
using Our.Umbraco.Gandalf.Core.Repositories;
using Our.Umbraco.Gandalf.Core.Services;

namespace Our.Umbraco.Gandalf.Core.StartUp
{
	public class GandalfComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register(typeof(IAllowedIpService), typeof(AllowedIpService));
			composition.Register(typeof(IRepository), typeof(AllowedIpRepository));

			composition.ContentFinders().Insert<NotAllowedLastChangeContentFinder>(0);
			composition.ContentFinders().Insert<AllowedIpContentFinder>(1);
		}
	}
}
