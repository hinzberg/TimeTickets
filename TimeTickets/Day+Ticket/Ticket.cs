using System;
using System.Diagnostics;

namespace TimeTickets.TimeTicket
{
    public class Ticket
    {
        private int _elapsedSecondsPreviousRun;
        public string Description { get; set; }
        public Stopwatch Stopwatch { get; set; }
        public Guid Id { get; set; }

        public int TotalElapsedSeconds
        {
            get
            {
                var currentRun = (int)Stopwatch.ElapsedMilliseconds / 1000;
                return currentRun + _elapsedSecondsPreviousRun;
            }
        }

        public Ticket()
        {
            Id = Guid.NewGuid();
            _elapsedSecondsPreviousRun = 0;
        }

        public void Start()
        {
            if (Stopwatch != null)
                _elapsedSecondsPreviousRun += (int)Stopwatch.ElapsedMilliseconds / 1000;
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }

        public void Stop()
        {
            Stopwatch.Stop();
        }
    }
}
