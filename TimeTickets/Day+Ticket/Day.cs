﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTickets.TimeTicket
{
    public class Day
    {
        private readonly List<Ticket> _tickets;
        public DateTime Date { get; set; }

        public Day()
        {
            _tickets = new List<Ticket>();
            Date = DateTime.Today;
        }

        public int AllElapsedSeconds { get { return _tickets.Sum(a => a.TotalElapsedSeconds); } }

        public void AddTicket(Ticket ticket)
        {
            StopOtherTickets(ticket);
            _tickets.Add(ticket);
        }

        public void RemoveTicket(Ticket ticket)
        {
            ticket.Stop();
            _tickets.Remove(ticket);
        }

        public void ClearTickets()
        {
            foreach (var ticket in _tickets)
                ticket.Stop();

            _tickets.Clear();
        }

        public List<Ticket> GetAllTickets()
        {
            return _tickets;
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
