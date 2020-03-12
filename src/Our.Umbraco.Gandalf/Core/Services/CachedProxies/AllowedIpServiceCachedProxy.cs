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
			_runtimeCache.ClearByRegex($"{typeof(AllowedIpServiceCachedProxy)}(.*)");

			return result;
		}

		public void Delete(int id)
		{
			_runtimeCache.ClearByRegex($"{typeof(AllowedIpServiceCachedProxy)}_(.*)");
			this._allowedIpService.Delete(id);
		}

		public IEnumerable<AllowedIpDto> GetAll()
		{
			var cacheKey = $"{typeof(AllowedIpServiceCachedProxy)}";
			return _runtimeCache.GetCacheItem(cacheKey, () => _allowedIpService.GetAll());
		}

		public AllowedIpDto GetById(int id)
		{
			return _allowedIpService.GetById(id);
		}

		public AllowedIpDto GetByIpAddress(string ipAddress)
		{
			var cacheKey = $"{typeof(AllowedIpServiceCachedProxy)}";
			return _runtimeCache.GetCacheItem(cacheKey, () => GetByIpAddress(ipAddress));		
		}

		public KeyValueDto GetStatus()
		{
			return this._allowedIpService.GetStatus();
		}

		public AllowedIpDto Update(AllowedIpDto poco)
		{
			var result = this._allowedIpService.Update(poco);
			_runtimeCache.ClearByRegex($"{typeof(AllowedIpServiceCachedProxy)}(.*)");
			
			return result;

		}

		public KeyValueDto UpdateAppStatus(string value)
		{
			var result = this._allowedIpService.UpdateAppStatus(value);
			_runtimeCache.ClearByRegex($"{typeof(AllowedIpServiceCachedProxy)}_(.*)");
			return result;
		}

	}
}
