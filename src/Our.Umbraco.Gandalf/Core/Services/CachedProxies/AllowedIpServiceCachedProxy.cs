using System.Collections.Generic;
using Our.Umbraco.Gandalf.Core.Models.DTOs;
using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using System.Linq;
using Umbraco.Core.Cache;

namespace Our.Umbraco.Gandalf.Core.Services.CachedProxies
{
	public class AllowedIpServiceCachedProxy : IAllowedIpService
	{
		private readonly IAllowedIpService _allowedIpService;
		private readonly IAppCache _runtimeCache;

		public AllowedIpServiceCachedProxy(AllowedIpService allowedIpService, AppCaches appCaches)
		{
			_allowedIpService = allowedIpService;
			_runtimeCache = appCaches.RuntimeCache;
		}

		public AllowedIpDto Create(string ipAddress, string notes)
		{
			var result = this._allowedIpService.Create(ipAddress, notes);
			ClearCache();

			return result;
		}

		public void Delete(int id)
		{
			ClearCache();
			this._allowedIpService.Delete(id);
		}

		public IEnumerable<AllowedIpDto> GetAll()
		{
			return _allowedIpService.GetAll();
		}

		public AllowedIpDto GetById(int id)
		{
			return _allowedIpService.GetById(id);
		}

		public AllowedIpDto GetByIpAddress(string ipAddress)
		{
			var cacheKey = $"{typeof(AllowedIpServiceCachedProxy)}_{ipAddress}";

			return _runtimeCache.GetCacheItem(cacheKey, () => _allowedIpService.GetByIpAddress(ipAddress));
		}

		public KeyValueDto GetStatus()
		{
			return this._allowedIpService.GetStatus();
		}

		public AllowedIpDto Update(AllowedIpDto poco)
		{
			var result = this._allowedIpService.Update(poco);
			ClearCache();

			return result;

		}

		public KeyValueDto UpdateAppStatus(string value)
		{
			var result = this._allowedIpService.UpdateAppStatus(value);
			ClearCache();

			return result;
		}

		private void ClearCache()
		{
			_runtimeCache.ClearByRegex($"{typeof(AllowedIpServiceCachedProxy)}_(.*)");
		}
	}
}
