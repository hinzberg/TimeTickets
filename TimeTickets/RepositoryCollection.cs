using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTickets.Day_Ticket;
using TimeTickets.RecuringTasks;

namespace TimeTickets
{
    /// <summary>
    /// A global singleton for all Repositories.
    /// Maybe not the best design pattern out there but works fine.
    /// </summary>
    public class RepositoryCollection
    {
        public RecuringTasksRepository RecuringTasksRepository { get; set; }
        public DayRepository DayRepository { get; set; }

        private static RepositoryCollection _instance = null;

        private RepositoryCollection()
        {
            RecuringTasksRepository = new RecuringTasksRepository();
            DayRepository = new DayRepository();
        }

        public static RepositoryCollection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RepositoryCollection();
                return _instance;
            }
        }

        public string GetWorkingPath()
        {
            string executingPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string workingPath = System.IO.Path.GetDirectoryName(executingPath);
            return workingPath;
        }

        public void Load()
        {
            DayRepository.Load(System.IO.Path.Combine(GetWorkingPath(), "TimeTicketDays.xml"));
        }

        public void Save()
        {
            DayRepository.Save(System.IO.Path.Combine(GetWorkingPath(), "TimeTicketDays.xml"));            
        }
    }
}
