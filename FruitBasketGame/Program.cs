using FruitBasketGame.Game;
using FruitBasketGame.Player;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace FruitBasketGame
{
    class Program
    {
        private const string InputDataExample = "5 Vasia Random Kolia Memory Roma Thorough Lena Cheater Vania ThoroughCheater";
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            Dictionary<string, PlayerType> playersData = ReadPlayersData(args);

            Rules rules = ReadRules();
            var game = new GameDirector(rules);
            game.SetUpNewGame(playersData);
            int number = game.GuessNumber();
            Console.WriteLine("Guessed number is " + number);
            game.Start();
            game.FinishGame.WaitOne();

            IPlayer winner = game.Winner;

            if (winner != null)
                Console.WriteLine("Winner is " + winner.Name);
            else
                Console.WriteLine("Closest attempt was " + game.BestAttempt.Number + " made by " + game.BestAttempt.Player.Name);

            Console.ReadLine();
        }

        

        private static Dictionary<string, PlayerType> ReadPlayersData(string[] args)
        {
            try
            {
                int playersCount = int.Parse(args[0]);

                if (playersCount < 2 || playersCount > 8)
                    throw new ArgumentException("Incorrect players count. Must be between 2 and 8");

                var result = new Dictionary<string, PlayerType>();

                for (int i = 1; i < args.Length; i+=2)
                {
                    string name = args[i];
                    PlayerType type = (PlayerType)Enum.Parse(typeof(PlayerType), args[i + 1]);
                    result.Add(name, type);
                }

                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine("Incorrect input data\nInput data example:\n" + InputDataExample);
                throw new ApplicationException("Fatal Error\nIncorrect input data", e);
            }
        }

        private static Rules ReadRules()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                int minPossibleNumber = int.Parse(config.AppSettings.Settings["MinPossibleNumber"].Value);
                int maxPossibleNumber = int.Parse(config.AppSettings.Settings["MaxPossibleNumber"].Value);
                int maxAttempts = int.Parse(config.AppSettings.Settings["MaxAttempts"].Value);
                int maxGameDurationMs = int.Parse(config.AppSettings.Settings["MaxGameDurationMs"].Value);

                return new Rules(minPossibleNumber, maxPossibleNumber, maxAttempts, maxGameDurationMs);
            }
            catch(Exception ex)
            {
                //Log exception and return default rules
                return new Rules(40, 140, 100, 1500);
            }

        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            Console.WriteLine("Fatal Error happend\n" + ex != null ? ex.Message : e.ExceptionObject.ToString());
        }
    }
}
