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
			_runtimeCache.ClearByRegex($"{typeof(AllowedIpServiceCachedProxy)}_(.*)");
			return this._allowedIpService.Create(ipAddress, notes);
		}

		public void Delete(int id)
		{
			this._allowedIpService.Delete(id);
			_runtimeCache.ClearByRegex($"{typeof(AllowedIpServiceCachedProxy)}_(.*)");
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
			return GetAll().FirstOrDefault(a =>a.IpAddress.Equals(ipAddress));
		}

		public KeyValueDto GetStatus()
		{
			return this._allowedIpService.GetStatus();
		}

		public AllowedIpDto Update(AllowedIpDto poco)
		{
			_runtimeCache.ClearByRegex($"{typeof(AllowedIpServiceCachedProxy)}_(.*)");
			return this._allowedIpService.Update(poco);
		}

		public KeyValueDto UpdateAppStatus(string value)
		{
			_runtimeCache.ClearByRegex($"{typeof(AllowedIpServiceCachedProxy)}_(.*)");
			return this._allowedIpService.UpdateAppStatus(value);
		}

	}
}
