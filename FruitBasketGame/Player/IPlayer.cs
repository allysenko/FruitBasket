using FruitBasketGame.Game;
using System.Threading.Tasks;

namespace FruitBasketGame.Player
{
    public interface IPlayer
    {
        void Register(IReferee referee);
        void SetRules(Rules rules);
        Task StartGuess();
        string Name { get; }
    }
}
