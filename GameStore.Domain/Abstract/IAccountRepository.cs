using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Domain.Concrete;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Abstract
{
	public interface IAccountRepository
	{
		EfDbContext Context { get; }

		IEnumerable<Administrator> Administrators { get; }

		Task<bool> IsAdminExistsAsync(Administrator administrator);

		Task AddAdminAsync(Administrator administrator);

		Task<Administrator> LoginAsync(Administrator administrator, string password);
	}
}