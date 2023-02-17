using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTickets.RecuringTasks
{
    public class RecuringTask
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public RecuringTask()
        {
            this.Id = Guid.NewGuid();
            this.Description = string.Empty;
        }
    }
}
