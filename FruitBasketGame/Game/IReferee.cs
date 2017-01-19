using FruitBasketGame.Player;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FruitBasketGame.Game
{
    public interface IReferee
    {
        void Initialize(Rules rules);
        int GuessNumber();
        IReadOnlyList<Attempt> Attempts { get; }
        IPlayer Winner { get; }
        Task<bool> TryGuess(Attempt attempt);
        void Start();
        ManualResetEvent FinishGame { get; }
        Attempt BestAttempt { get; }
    }
}
