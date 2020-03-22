using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Threading.Tasks;
using GameStore.Domain.Entities;
using GameStore.Domain.Abstract;
using System.Linq;

namespace GameStore.Domain.Concrete
{
	public class EfGameRepository : IGameRepository
	{
		private readonly EfDbContext _context;

		public EfGameRepository(EfDbContext context)
		{
			_context = context;
		}

		public async Task AddGameAsync(Game game)
		{
			_context.Set<Game>().AddOrUpdate(game);
			await _context.SaveChangesAsync();

			//await _context.Database.ExecuteSqlCommandAsync("EXEC [dbo].[Games_Add] @id, @name, @description, @price, @releaseDate, @gameImage, @backImage, @previewLink, @publisherId, @categoryId, @quantity, @shortId",
			//		new SqlParameter("@id", game.GameId),
			//		new SqlParameter("@name", game.Name),
			//		new SqlParameter("@description", game.Description),
			//		new SqlParameter("@price", game.Price),
			//		new SqlParameter("@releaseDate", game.ReleaseDate),
			//		new SqlParameter("@gameImage", (object)game.GameImage ?? DBNull.Value),
			//		new SqlParameter("@backImage", (object)game.BackImage ?? DBNull.Value),
			//		new SqlParameter("@previewLink", (object)game.PreviewLink ?? DBNull.Value),
			//		new SqlParameter("@publisherId", game.PublisherId),
			//		new SqlParameter("@categoryId", game.CategoryId),
			//		new SqlParameter("@quantity", game.Quantity),
			//		new SqlParameter("@shortId", game.ShortId));
		}

		public async Task UpdateGameAsync(Game game)
		{
			_context.Set<Game>().AddOrUpdate(game);
			await _context.SaveChangesAsync();

			//await _context.Database.ExecuteSqlCommandAsync("EXEC [dbo].[Games_Update] @id, @name, @description, @price, @releaseDate, @gameImage, @backImage, @previewLink, @publisherId, @categoryId, @quantity",
			//	new SqlParameter("@id", game.GameId),
			//	new SqlParameter("@name", game.Name),
			//	new SqlParameter("@description", game.Description),
			//	new SqlParameter("@price", game.Price),
			//	new SqlParameter("@releaseDate", game.ReleaseDate),
			//	new SqlParameter("@gameImage", (object)game.GameImage ?? DBNull.Value),
			//	new SqlParameter("@backImage", (object)game.BackImage ?? DBNull.Value),
			//	new SqlParameter("@previewLink", (object)game.PreviewLink ?? DBNull.Value),
			//	new SqlParameter("@publisherId", game.PublisherId),
			//	new SqlParameter("@categoryId", game.CategoryId),
			//	new SqlParameter("@quantity", game.Quantity));
		}

		public async Task AddOrderAsync(Customer customer, Cart cart)
		{
			List<Order> orders = new List<Order>();

			var games = cart.Lines.Join(_context.Games, g => g.Game.GameId, c => c.GameId,
				(g, c) => new { Game = c, Qty = g.Quantity }).ToList();

			_context.Set<Customer>().AddOrUpdate(customer);

			foreach (var line in games)
			{
				orders.Add(new Order { Customer = customer, Game = line.Game, Quantity = line.Qty });
			}

			_context.Orders.AddRange(orders);

			await _context.SaveChangesAsync();

			cart.Clear();
		}

		public async Task AddRequirementsAsync(Requirements requirements)
		{
			_context.Set<Requirements>().AddOrUpdate(requirements);
			await _context.SaveChangesAsync();
		}

		public async Task<Game> DeleteGameAsync(Guid id)
		{
			Game game = GetGameById(id);

			if (game != null)
			{
				_context.Set<Game>().Remove(game);
				await _context.SaveChangesAsync();
				//await _context.Database.ExecuteSqlCommandAsync("EXEC [dbo].[Games_DeleteById] @id", new SqlParameter("id", game.GameId));
			}

			return game;
		}

		public IEnumerable<Game> GetAllGames()
		{
			return _context.Games.SqlQuery(@"EXEC [dbo].[Games_GetAll]");
		}

		public IDictionary<Guid, string> GetCategories()
		{
			return _context.Publishers.SqlQuery("SELECT Id, Name FROM dbo.Categories")
									  .ToDictionary(k => k.Id, v => v.Name);
		}

		public IDictionary<Guid, string> GetPublishers()
		{
			return _context.Publishers.SqlQuery("SELECT Id, Name FROM dbo.Publishers")
									  .ToDictionary(k => k.Id, v => v.Name);
		}

		public IEnumerable<string> GetCategoryNames()
		{
			return _context.Categories.SqlQuery("EXEC [dbo].[Categories_GetAllNames]").Select(x => x.Name);
		}

		public IEnumerable<Game> GetReleasedGames()
		{
			return _context.Games.SqlQuery("EXEC [dbo].[Games_GetReleased]");
		}

		public IEnumerable<Game> GetComingSoonGames()
		{
			return _context.Games.SqlQuery("SELECT * FROM dbo.Games_ComingSoon");
		}

		public IEnumerable<Game> GetByCategory(string category)
		{
			return _context.Games.SqlQuery("EXEC [dbo].[Games_GetByCategory] @category", new SqlParameter("category", category));
		}

		public Game GetGameById(Guid id)
		{
			return _context.Games.SqlQuery("EXEC [dbo].[Games_GetById] @id", new SqlParameter("id", id))?.FirstOrDefault();
		}

		public Game GetGameByShortId(int shortId)
		{
			return _context.Games.SqlQuery("EXEC [dbo].[Games_GetByShortId] @shortId", new SqlParameter("shortId", shortId))?.FirstOrDefault();
		}

		public Category GetCategoryById(Guid id)
		{
			return _context.Categories.SqlQuery("EXEC [dbo].[Categories_GetById] @id", new SqlParameter("id", id))?.FirstOrDefault();
		}

		public Publisher GetPublisherById(Guid id)
		{
			return _context.Publishers.SqlQuery("EXEC [dbo].[Publishers_GetById] @id", new SqlParameter("id", id))?.FirstOrDefault();
		}

		public Requirements GetRequirementsByGameId(Guid gameId)
		{
			return _context.Requirements.SqlQuery("EXEC [dbo].[Requirements_GetByGameId] @gameId", new SqlParameter("gameId", gameId))?.FirstOrDefault();
		}

		public IEnumerable<Game> GetGamesByName(string name)
		{
			return _context.Games.SqlQuery("EXEC [dbo].[Games_GetByName] @name", new SqlParameter("name", name));
		}

		public int GetCountOfGames()
		{
			return _context.Database.SqlQuery<int>("SELECT [dbo].[GetCountOfGames]()").FirstOrDefault();
		}
	}
}