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

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        public bool CanSwitch = true;
        private ICommand _switchCommand;
        public ICommand SwitchCommand => _switchCommand ?? (_switchCommand = new CommandHandler(SwitchCommandAction, () => CanSwitch));

        public bool CanEdit => true; // always
        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ?? (_editCommand = new CommandHandler(EditCommandAction, () => CanEdit));

        public Action<Ticket> StopOtherTicketsAction { get; set; }

        public TimeTicketViewModel(Ticket ticket)
        {
            _ticket = ticket;
            _ticket.RunningStateChanged = RunningStateChanged;
            DurationTime = "00:00:00";
            Description = ticket.Description;
        }

        public void RunningStateChanged(bool isRunning)
        {
            IsRunning = isRunning;
        }

        /// <summary>
        /// Is triggerd from the main timer in the MainWindow
        /// </summary>
        public void UpdateDurationTime()
        {
            TimeSpan ts = new TimeSpan(0, 0, 0,_ticket.TotalElapsedSeconds, 0);
            DurationTime = ts.ToString(@"hh\:mm\:ss");
        }

        private void SwitchCommandAction()
        {
            if (IsRunning)
                Stop();
            else
                Start();
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

        private void EditCommandAction()
        {
        }

        public Ticket GetModel()
        {
            return _ticket;
        }
    }
}
