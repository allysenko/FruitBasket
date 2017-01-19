using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using FruitBasketGame.Player;

namespace FruitBasketGame.Game
{
    public class FruitBasketGameReferee : IReferee
    {
        private object _lockObj;
        private Timer _timer;
        private bool _isGameFinished { get { return FinishGame.WaitOne(0); } }
        private int _guessedNumber;
        private List<Attempt> _attempts;
        private Rules _rules;
        public ManualResetEvent FinishGame { get; private set; }
        
        public IPlayer Winner { get; private set; }

        public IReadOnlyList<Attempt> Attempts
        {
            get
            {
                lock(_lockObj)
                {
                    List<Attempt> copy = new List<Attempt>(_attempts);
                    return copy.AsReadOnly();
                }
            }
        }

        public Attempt BestAttempt
        {
            get
            {
                Attempt best = _attempts.First();
                int closestAttemptDifference = Math.Abs(_guessedNumber - best.Number);

                foreach (Attempt a in _attempts)
                {
                    int currentDifference = Math.Abs(_guessedNumber - a.Number);
                    if (currentDifference < closestAttemptDifference)
                    {
                        best = a;
                        closestAttemptDifference = currentDifference;
                    }
                }

                return best;
            }
        }

        public FruitBasketGameReferee()
        {
            _lockObj = new object();
            _attempts = new List<Attempt>();
            FinishGame = new ManualResetEvent(false);
        }

        public void Initialize(Rules rules)
        {
            _rules = rules;
        }

        public void Start()
        {
            _timer = new Timer((o) => FinishGame.Set(), null, _rules.MaxGameDurationMs, Timeout.Infinite);
        }

        public int GuessNumber()
        {
            _guessedNumber = new Random().Next(_rules.MinPossibleNumber, _rules.MaxPossibleNumber);
            return _guessedNumber;
        }

        public async Task<bool> TryGuess(Attempt attempt)
        {
            lock(_lockObj)
            {
                if (_isGameFinished)
                    throw new ApplicationException("Game finished");//it's better to throw special exception

                if(attempt.Number ==_guessedNumber)
                {
                    Winner = attempt.Player;
                    FinishGame.Set();
                    return true;
                }
                
                _attempts.Add(attempt);

                if (_attempts.Count >= _rules.MaxAttempts)
                {
                    FinishGame.Set();
                    throw new ApplicationException("Game finished");//it's better to throw special exception
                }
            }

            await HoldPenalty(_guessedNumber, attempt.Number);
            return false;
        }

        private async Task HoldPenalty(int guessedNumber, int attemptNumber)
        {
            await Task.Delay(Math.Abs(guessedNumber - attemptNumber));
        }
    }
}
