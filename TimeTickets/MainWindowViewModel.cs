using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToolkit;

namespace TimeTickets
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<TimeTicketViewModel> TimeTickets { get; set; }
        public TimeTicketViewModel SelectedTimeTicketVM { get; set; }
        public TimeTicketViewModel CurrentlyRunningTimeTicketVM { get; set; }

        public bool CanExecuteClearAll => true; // always
        private ICommand _clearAllCommand;
        public ICommand ClearAllCommand => _clearAllCommand ?? (_clearAllCommand = new CommandHandler(ClearAllAction, () => CanExecuteClearAll));

        public bool CanExecuteNewTask => true; // always
        private ICommand _newTaskCommand;
        public ICommand NewTaskCommand => _newTaskCommand ?? (_newTaskCommand = new CommandHandler(NewTaskAction, () => CanExecuteNewTask));

        public MainWindowViewModel()
        {
            this.TimeTickets = new ObservableCollection<TimeTicketViewModel>();
        }

        private void ClearAllAction()
        {
            //todo confirm messagebox 

            foreach (var timeTicket in TimeTickets)
                timeTicket.Stop();

            TimeTickets.Clear();            
        }

        private void NewTaskAction()
        {
            if (this.CurrentlyRunningTimeTicketVM != null)
                this.CurrentlyRunningTimeTicketVM.Stop();

            var ticket = new TimeTicketViewModel();
            ticket.Start();
            this.CurrentlyRunningTimeTicketVM = ticket;
            this.TimeTickets.Insert(0, ticket);
        }
    }
}
