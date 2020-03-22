using System.Collections.Generic;
using GameStore.Domain.Entities;
using PagedList;

namespace GameStore.WebUI.Models
{
    public class GamesListViewModel
    {
        public GamesListViewModel()
        {
            Sorts = new Dictionary<string, string>
            {
                ["price-asc"] = "Price (Low to High)",
                ["price-desc"] = "Price (High to Low)",
                ["title-asc"] = "Title (A to Z)",
                ["title-desc"] = "Title (Z to A)",
                ["date-asc"] = "Date (New to Old)",
                ["date-desc"] = "Date (Old to New)",
            };
        }

        public string Name { get; set; }
        public string Genre { get; set; }
        public string SortBy { get; set; }
        public IPagedList<Game> PagedGames { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IDictionary<string, string> Sorts { get; }
    }
}