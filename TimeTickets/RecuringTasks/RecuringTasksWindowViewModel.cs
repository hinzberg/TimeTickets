using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTickets.RecuringTasks
{
    public class RecuringTasksWindowViewModel
    {
        public ObservableCollection<RecuringTask> RecuringTasks { get; set; }

        public RecuringTasksWindowViewModel()
        {
            RecuringTasks = RepositoryCollection.Instance.RecuringTasksRepository.RecuringTasks;
        }
    }
}
