using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTickets.RecuringTasks
{
    public class RecuringTasksRepository : IRepository<RecuringTask>
    {
        public  ObservableCollection<RecuringTask> RecuringTasks { get; set; }

        public RecuringTasksRepository()
        {
            RecuringTasks = new ObservableCollection<RecuringTask>();

            RecuringTask task = new RecuringTask();
            task.Description = "Sample";
            Add(task);
        }

        public void Add(RecuringTask item)
        {
            RecuringTasks.Add(item);
        }

        public bool Remove(RecuringTask item)
        {
            return RecuringTasks.Remove(item);
        }
    }
}
