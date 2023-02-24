using System;
using System.Windows.Input;
using System.Windows.Threading;
using WPFToolkit;

namespace TimeTickets
{
    public class TimeTicketViewModel : ViewModelBase
    {
        private readonly TimeTicket.Day _day;
        private readonly TimeTicket.Ticket _ticket;
        private DispatcherTimer _dispatcherTimer;

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

        public bool CanPause => _ticket.Stopwatch != null && _ticket.Stopwatch.IsRunning;
        private ICommand _pauseCommand;
        public ICommand PauseCommand => _pauseCommand ?? (_pauseCommand = new CommandHandler(PauseAction, () => CanPause));

        public bool CanRun => _ticket.Stopwatch != null && !_ticket.Stopwatch.IsRunning;
        private ICommand _runCommand;
        public ICommand RunCommand => _runCommand ?? (_runCommand = new CommandHandler(RunAction, () => CanRun));

        public bool CanEdit => true; // always
        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ?? (_editCommand = new CommandHandler(EditAction, () => CanEdit));

        public TimeTicketViewModel(TimeTicket.Day day, TimeTicket.Ticket ticket)
        {
            _day = day;
            _ticket = ticket;
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(OnDispatcherTimerTick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            DurationTime = "00:00:00";
            Description = "Edit Description";
        }

        public void Start()
        {
            _dispatcherTimer.Start();
            _day.StopOtherTickets(this._ticket);
            _ticket.Start();
        }

        public void Stop()
        {
            _ticket.Stop();
            _dispatcherTimer.Stop();
        }

        private void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0,_ticket.TotalElapsedSeconds, 0);
            DurationTime = ts.ToString(@"hh\:mm\:ss");
        }

        private void PauseAction()
        {
            this.Stop();
        }

        private void RunAction()
        {
            this.Start();
        }

        private void EditAction()
        {
        }
    }
}
