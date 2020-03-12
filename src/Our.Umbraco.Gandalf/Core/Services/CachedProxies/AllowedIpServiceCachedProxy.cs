using System.Collections.Generic;
using DangEasy.Interfaces.Caching;
using DangEasy.Caching.MemoryCache;
using Our.Umbraco.Gandalf.Core.Models.DTOs;
using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using System.Linq;
using Umbraco.Core.Cache;

namespace Our.Umbraco.Gandalf.Core.Services.CachedProxies
{
	public class AllowedIpServiceCachedProxy : IAllowedIpService
	{

		private readonly ICache _cache;
		private readonly IAllowedIpService _allowedIpService;
		private readonly IAppCache _runtimeCache;

		public AllowedIpServiceCachedProxy(AllowedIpService allowedIpService, ICache cache, AppCaches appCaches)
		{
			_allowedIpService = allowedIpService;
			_cache = cache;
			_runtimeCache = appCaches.RuntimeCache;
		}
	
		public AllowedIpDto Create(string ipAddress, string notes)
		{
			return this._allowedIpService.Create(ipAddress, notes);
		}

		public void Delete(int id)
		{
			this._allowedIpService.Delete(id);
			//need to make this clear the cache
			_runtimeCache.ClearByKey($"{typeof(AllowedIpServiceCachedProxy)}_(.*)");
		}

		public IEnumerable<AllowedIpDto> GetAll()
		{
			var cacheKey = CacheKey.Build<AllowedIpServiceCachedProxy, IEnumerable<AllowedIpDto>>("GetAll");
			return _cache.Get(cacheKey, () => _allowedIpService.GetAll());
		}

		//doesn't need to be cached as it might only be called by the back office
		public AllowedIpDto GetById(int id)
		{
			return _allowedIpService.GetById(id);
		}

		// as this is getting called in the handler I should cache this one
		public AllowedIpDto GetByIpAddress(string ipAddress)
		{
			var cacheKey = CacheKey.Build<AllowedIpServiceCachedProxy, AllowedIpDto>(ipAddress);
			return GetAll().FirstOrDefault(a =>a.IpAddress.Equals(ipAddress));
		}

		public KeyValueDto GetStatus()
		{
			return this._allowedIpService.GetStatus();
		}

		public AllowedIpDto Update(AllowedIpDto poco)
		{
			return this._allowedIpService.Update(poco);
		}

		public KeyValueDto UpdateAppStatus(string value)
		{
			return this._allowedIpService.UpdateAppStatus(value);
		}

		// clear cache in the service any time there is a set, AND on a toggle

		// getall response should be cached
	}
}
