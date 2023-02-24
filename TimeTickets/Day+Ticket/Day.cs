using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTickets.TimeTicket
{
    public class Day : IRepository
    {
        private readonly List<Ticket> _tickets;

        public Day()
        {
            _tickets = new List<Ticket>();
        }

        public int AllElapsedSeconds { get { return _tickets.Sum(a => a.TotalElapsedSeconds); } }

        public void AddTicket(Ticket ticket)
        {
            StopOtherTickets(ticket);
            _tickets.Add(ticket);
        }

        /// <summary>
        /// If this ticket is started or resumed all othet tickets must be stopped.
        /// </summary>
        public void StopOtherTickets(Ticket currentTicket)
        {
            var otherTickets = _tickets.Where(a => a.Id != currentTicket.Id);
            foreach (var ticket in otherTickets)
                ticket.Stop();
        }
    }
}
