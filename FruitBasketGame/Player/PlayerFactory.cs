using System;
using System.Collections.Generic;

namespace FruitBasketGame.Player
{
    public class PlayerFactory
    {
        //it's better to fill factory outside
        private static Dictionary<PlayerType, Func<string, IPlayer>> _activators = new Dictionary<PlayerType, Func<string, IPlayer>>
        {
            { PlayerType.Random, (name) => new RandomGuesser(name) },
            { PlayerType.Memory, (name) => new MemoryRandomGuesser(name) },
            { PlayerType.Thorough, (name) => new ThoroughGuesser(name) },
            { PlayerType.Cheater, (name) => new CheaterRandomGuesser(name) },
            { PlayerType.ThoroughCheater, (name) => new CheaterThoroughGuesser(name) },
        };

        public static IPlayer CreatePlayer(string name, PlayerType type)
        {
            Func<string, IPlayer> activator;

            if (_activators.TryGetValue(type, out activator))
                return activator(name);
            else
                throw new ApplicationException("Unsupported player type " + type);
            
        }
    }
}
