using System.Collections.Generic;
using Our.Umbraco.Gandalf.Core.Models.Pocos;

namespace Our.Umbraco.Gandalf.Core.Repositories
{
	public interface IRepository
	{
		AllowedIp GetById(int id);
		AllowedIp GetByIpAddress(string ipAddress);
		AllowedIp Create(AllowedIp poco);
		IEnumerable<AllowedIp> GetAll();
		AllowedIp Update(AllowedIp poco);
		void Delete(AllowedIp poco);
	}
}