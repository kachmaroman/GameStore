using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.Domain.Concrete;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Abstract
{
    public interface IGameRepository
    {
        Task AddGameAsync(Game game);
        Task UpdateGameAsync(Game game);
        Task AddOrderAsync(Customer customer, Cart cart);
        Task AddRequirementsAsync(Requirements requirements);
        Task<Game> DeleteGameAsync(Guid gameId);

        int GetCountOfGames();
		IEnumerable<Game> GetAllGames();
        IDictionary<Guid, string> GetCategories();
        IDictionary<Guid, string> GetPublishers();
        IEnumerable<string> GetCategoryNames();
        IEnumerable<Game> GetReleasedGames();
        IEnumerable<Game> GetComingSoonGames();
		IEnumerable<Game> GetByCategory(string category);
        Game GetGameById(Guid id);
        Game GetGameByShortId(int shortId);
        Category GetCategoryById(Guid id);
        Publisher GetPublisherById(Guid id);
        Requirements GetRequirementsByGameId(Guid gameId);
        IEnumerable<Game> GetGamesByName(string name);
	}
}
