using System;
using System.Windows.Input;
using TimeTickets.Interfaces;
using TimeTickets.TimeTicket;
using WPFToolkit;

namespace TimeTickets
{
    public class TimeTicketViewModel : ViewModelBase, IViewModel<Ticket>
    {
        private readonly Ticket _ticket;

        private string _durationTime;
        public string DurationTime
        {
            get { return _durationTime; }
            set
            {
                _durationTime = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return _ticket.Description; }
            set
            {
                _ticket.Description = value;
                OnPropertyChanged();
            }
        }

        public bool CanPause => _ticket._stopwatch != null && _ticket._stopwatch.IsRunning;
        private ICommand _pauseCommand;
        public ICommand PauseCommand => _pauseCommand ?? (_pauseCommand = new CommandHandler(PauseCommandAction, () => CanPause));

        public bool CanRun => _ticket._stopwatch != null && !_ticket._stopwatch.IsRunning;
        private ICommand _runCommand;
        public ICommand RunCommand => _runCommand ?? (_runCommand = new CommandHandler(RunCommandAction, () => CanRun));

        public bool CanEdit => true; // always
        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ?? (_editCommand = new CommandHandler(EditCommandAction, () => CanEdit));

        public Action<TimeTicket.Ticket> StopOtherTicketsAction { get; set; }

        public TimeTicketViewModel(TimeTicket.Ticket ticket)
        {
            _ticket = ticket;
            DurationTime = "00:00:00";
            Description = ticket.Description;
        }

        public void Start()
        {
            StopOtherTicketsAction?.Invoke(_ticket);
            _ticket.Start();
        }

        public void Stop()
        {
            _ticket.Stop();
        }

        /// <summary>
        /// Is triggerd from the main timer in the MainWindow
        /// </summary>
        public void UpdateDurationTime()
        {
            TimeSpan ts = new TimeSpan(0, 0, 0,_ticket.TotalElapsedSeconds, 0);
            DurationTime = ts.ToString(@"hh\:mm\:ss");
        }

        private void PauseCommandAction()
        {
            this.Stop();
        }

        private void RunCommandAction()
        {
            this.Start();
        }

        private void EditCommandAction()
        {
        }

        public Ticket GetModel()
        {
            return _ticket;
        }
    }
}
