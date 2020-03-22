 using System.Collections.Generic;
using System.Linq;

namespace GameStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Game game, int quantity)
        {
            var line = lineCollection.FirstOrDefault(g => g.Game.GameId == game.GameId);

            if (line == null)
                lineCollection.Add(new CartLine(game, quantity));
            else
                line.Quantity += quantity;
        }

        public void RemoveLine(Game game) => lineCollection.RemoveAll(g => g.Game.GameId == game.GameId);

        public decimal ComputeTotalValue() => lineCollection.Sum(g => g.Game.Price * g.Quantity);

        public void Clear() => lineCollection.Clear();

        public IEnumerable<CartLine> Lines => lineCollection;
    }

    public class CartLine
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }

        public CartLine(Game game, int quantity)
        {
            Game = game;
            Quantity = quantity;
        }
    }
}
