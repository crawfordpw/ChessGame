using System;

namespace ChessModel
{
    public class PlayerClock
    {
        public DateTime TurnStartTime { get; private set; }
        public TimeSpan TimeLimit { get; set; }
        public TimeSpan TimeRemaining => TimeLimit - TimeElapsed;
        public bool IsTicking { get; private set; }
        private TimeSpan _timeElapsed;
        
        public PlayerClock() : this(new TimeSpan(0, 60, 0))
        {

        }

        public PlayerClock(TimeSpan timeLimit)
        {
            Reset();
            this.TimeLimit = timeLimit;
        }

        public TimeSpan TimeElapsed {
            get {
                return IsTicking ? _timeElapsed + (DateTime.Now - TurnStartTime) : _timeElapsed;
            }
            set {
                _timeElapsed = value;
            }
        }

        public void Reset()
        {
            _timeElapsed = new TimeSpan(0, 0, 0);
            TurnStartTime = DateTime.Now;
        }

        public void Start()
        {
            IsTicking = true;
            TurnStartTime = DateTime.Now;
        }

        public void Stop()
        {
            IsTicking = false;
            _timeElapsed += DateTime.Now - TurnStartTime;
        }
    }
}
