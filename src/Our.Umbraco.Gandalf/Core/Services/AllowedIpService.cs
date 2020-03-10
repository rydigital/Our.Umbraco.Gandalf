using Our.Umbraco.Gandalf.Core.Constants;
using Our.Umbraco.Gandalf.Core.Extensions;
using Our.Umbraco.Gandalf.Core.Models.DTOs;
using Our.Umbraco.Gandalf.Core.Models.Pocos;
using Our.Umbraco.Gandalf.Core.Repositories;
using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using Our.Umbraco.OpenKeyValue.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Gandalf.Core.Constants;

namespace Our.Umbraco.Gandalf.Core.Services
{
	public interface IAllowedIpService
	{
		AllowedIpDto GetById(int id);
		AllowedIpDto GetByIpAddress(string ipAddress);
		AllowedIpDto Create(string ipAddress, string notes);
		IEnumerable<AllowedIpDto> GetAll();
		AllowedIpDto Update(AllowedIpDto poco);
		KeyValueDto UpdateAppStatus(string key, string value);
		KeyValueDto GetStatus();
		void Delete(int id);
	}

	public class AllowedIpService : IAllowedIpService
	{
		private readonly IRepository _repository;
		private readonly IOpenKeyValueService _service;

		public AllowedIpService(IRepository repository, IOpenKeyValueService service)
		{
			_repository = repository;
			_service = service;
		}


		public AllowedIpDto Create(string ipAddress, string notes)
		{
			var poco = new AllowedIp()
			{
				IpAddress = ipAddress,
				LastUpdated = DateTime.Now.ToUniversalTime(),
				Notes = notes
			};

			return _repository.Create(poco).ToDto();
		}

		public void Delete(int id)
		{
			var poco = _repository.GetById(id);
			if (poco == null)
			{
				throw new ArgumentException("No item with an Id that matches " + id);
			}

			_repository.Delete(poco);
		}

		public AllowedIpDto GetByIpAddress(string ipAddress)
		{
			return _repository.GetByIpAddress(ipAddress).ToDto();
		}

		public IEnumerable<AllowedIpDto> GetAll()
		{
			return _repository.GetAll().Select(x => x.ToDto());
		}

		public AllowedIpDto GetById(int id)
		{
			return _repository.GetById(id).ToDto();
		}

		public AllowedIpDto Update(AllowedIpDto dto)
		{
			var poco = new AllowedIp()
			{
				Id = dto.Id,
				IpAddress = dto.IpAddress,
				Notes = dto.Notes,
				LastUpdated = DateTime.Now.ToUniversalTime()
			};

			return _repository.Update(poco).ToDto();
		}

		public KeyValueDto UpdateAppStatus(string key, string value)
		{
			var poco = new IsEnabled()
			{
				Key = key,
				Enabled = value
			};
			return this._service.Set(poco.Key, poco.Enabled);

		}
		public KeyValueDto GetStatus()
		{
			return _service.Get(GandalfConstants.Key);
		}
	}
}
