using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private static RepositoryCollection instance = null;

        private RepositoryCollection()
        {
            RecuringTasksRepository = new RecuringTasksRepository();
        }

        public static RepositoryCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new RepositoryCollection();
                return instance;
            }
        }
    }
}
