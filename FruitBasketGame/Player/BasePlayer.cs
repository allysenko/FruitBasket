using System;
using System.Threading.Tasks;
using FruitBasketGame.Game;

namespace FruitBasketGame.Player
{
    public abstract class BasePlayer : IPlayer
    {
        protected IReferee Referee;
        protected Rules Rules;

        public string Name { get; private set; }

        protected BasePlayer(string name)
        {
            Name = name;
        }
        public void Register(IReferee referee)
        {
            Referee = referee;
        }

        public void SetRules(Rules rules)
        {
            Rules = rules;
        }

        public async Task StartGuess()
        {
            try
            {
                PrepareToGuess();

                while (!await TryGuess(GetNextNumber()))
                {
                    ; //empty body
                }

                //is this case plaer is winner and we finish our thread
            }
            catch(ApplicationException ex)//it's better to catch special exception
            {
                //this is expected exception. In this case player is looser and we just finish our thread     
            }
            catch(Exception e)
            {
                //unexpected exception at leat we log it
            }
        }

        protected abstract void PrepareToGuess();

        protected abstract int GetNextNumber();

        protected async Task<bool> TryGuess(int number)
        {
            return await Referee.TryGuess(new Attempt(this, number));
        }
    }
}
