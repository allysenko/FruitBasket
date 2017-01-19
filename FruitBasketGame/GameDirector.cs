using FruitBasketGame.Game;
using FruitBasketGame.Player;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FruitBasketGame
{
    class GameDirector
    {

        private IReferee _referee;
        private List<IPlayer> _players;
        private Rules _rules;

        public ManualResetEvent FinishGame { get { return _referee.FinishGame; } }

        public GameDirector(Rules rules)
        {
            _rules = rules;
        }

        public IPlayer Winner { get { return _referee.Winner; } }
        public Attempt BestAttempt { get { return _referee.BestAttempt; } }

        public void SetUpNewGame(Dictionary<string,PlayerType> players)
        {
            _referee = CreateReferee(_rules);
            _players = CreatePlayers(players, _rules);

            foreach (IPlayer player in _players)
                player.Register(_referee);
        }

        public int GuessNumber()
        {
            return _referee.GuessNumber();
        }

        public void Start()
        {
            _referee.Start();

            foreach (var player in _players)
                Task.Factory.StartNew(player.StartGuess);
        }

        private IReferee CreateReferee(Rules rules)
        {
            var referee = new FruitBasketGameReferee();
            referee.Initialize(rules);
            return referee;
        }

        private List<IPlayer> CreatePlayers(Dictionary<string,PlayerType> players, Rules rules)
        {
            List<IPlayer> result = new List<IPlayer>(players.Count);

            foreach(var playerData in players)
            {
                var player = PlayerFactory.CreatePlayer(playerData.Key, playerData.Value);
                player.SetRules(rules);
                result.Add(player);
            }

            return result;
        }
    }
}
