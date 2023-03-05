using System;
using System.Diagnostics;

namespace TimeTickets.TimeTicket
{
    public class Ticket
    {
        private int _elapsedSecondsPreviousRun;
        public string Description { get; set; }
        public Stopwatch _stopwatch { get; set; }
        public Guid Id { get; set; }

        public int TotalElapsedSeconds
        {
            get
            {
                var currentRun = (int)_stopwatch.ElapsedMilliseconds / 1000;
                return currentRun + _elapsedSecondsPreviousRun;
            }
        }

        public Ticket()
        {
            Id = Guid.NewGuid();
            Description = "Edit Task Description";
            _elapsedSecondsPreviousRun = 0;
            _stopwatch = new Stopwatch();
        }

        public static Ticket Create(string id, string description, int elapsedSconds)
        {
            Ticket t = new Ticket();
            t.Id = new Guid(id);
            t.Description = description;
            t.AddToElapsedSecondsPreviousRun(elapsedSconds);
            return t;
        }

        public void AddToElapsedSecondsPreviousRun(int seconds)
        {
            _elapsedSecondsPreviousRun = seconds;
        }

        public void Start()
        {
            if (_stopwatch != null)
                _elapsedSecondsPreviousRun += (int)_stopwatch.ElapsedMilliseconds / 1000;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch?.Stop();
        }
    }
}
