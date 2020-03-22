using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Concrete
{
	public class EfAccountRepository : IAccountRepository
	{
		private readonly EfDbContext _context;

		public EfAccountRepository()
		{
			_context = new EfDbContext();
		}

		public EfDbContext Context => _context;

		public IEnumerable<Administrator> Administrators => Context.Administrators;

		public async Task<bool> IsAdminExistsAsync(Administrator administrator)
		{
			return await Context.Administrators.AnyAsync(a => a.Login == administrator.Login);
		}

		public async Task AddAdminAsync(Administrator administrator)
		{
			_context.Administrators.Add(administrator);
			await _context.SaveChangesAsync();
		}

		public async Task<Administrator> LoginAsync(Administrator administrator, string password)
		{
			return await Context.Administrators.FirstOrDefaultAsync(a => a.Login == administrator.Login && a.Password == password);
		}
	}
}
