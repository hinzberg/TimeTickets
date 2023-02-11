using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using WPFToolkit;

namespace TimeTickets
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DispatcherTimer _dispatcherTimer;
        public ObservableCollection<TimeTicketViewModel> TimeTickets { get; set; }
        public TimeTicketViewModel SelectedTimeTicketVM { get; set; }

        public bool CanExecuteClearAll => true; // always
        private ICommand _clearAllCommand;
        public ICommand ClearAllCommand => _clearAllCommand ?? (_clearAllCommand = new CommandHandler(ClearAllAction, () => CanExecuteClearAll));

        public bool CanExecuteNewTask => true; // always
        private ICommand _newTaskCommand;
        public ICommand NewTaskCommand => _newTaskCommand ?? (_newTaskCommand = new CommandHandler(NewTaskAction, () => CanExecuteNewTask));

        public bool CanExecuteRenameTask => SelectedTimeTicketVM != null;
        private ICommand _renameTaskCommand;
        public ICommand RenameTaskCommand => _renameTaskCommand ?? (_renameTaskCommand = new CommandHandler(RenameAction, () => true));

        public bool CanDeleteTask => SelectedTimeTicketVM != null;
        private ICommand _deleteTaskCommand;
        public ICommand DeleteTaskCommand => _deleteTaskCommand ?? (_deleteTaskCommand = new CommandHandler(DeleteAction, () => true));

        private string _totalDurationTime;
        public string TotalDurationTime
        {
            get { return _totalDurationTime; }
            set
            {
                _totalDurationTime = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            this.TimeTickets = new ObservableCollection<TimeTicketViewModel>();
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(OnDispatcherTimerTick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
        }

        private void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            int allElapsedSeconds = TimeTickets.Sum(a => a.TotalElapsedSeconds);
            TimeSpan ts = new TimeSpan(0, 0, 0, allElapsedSeconds, 0);
            TotalDurationTime = ts.ToString(@"hh\:mm\:ss");
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
            var ticket = new TimeTicketViewModel(this.TimeTickets);
            ticket.Start();
            this.TimeTickets.Insert(0, ticket);
        }

        private void RenameAction()
        {
            RenameTimeTicketWindow vm = new RenameTimeTicketWindow(SelectedTimeTicketVM);
            vm.ShowDialog();
        }

        private void DeleteAction()
        {
            this.SelectedTimeTicketVM.Stop();
            this.TimeTickets.Remove(this.SelectedTimeTicketVM);
        }
    }
}
