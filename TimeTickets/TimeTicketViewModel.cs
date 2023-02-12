using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using WPFToolkit;

namespace TimeTickets
{
    public class TimeTicketViewModel : ViewModelBase
    {
        public readonly Guid Id;
        private int _elapsedSecondsCurrentRun;
        private int _elapsedSecondsPreviousRun;
        private string _durationTime;
        private string _description;
        private Stopwatch _stopwatch;
        private DispatcherTimer _dispatcherTimer;
        private readonly ObservableCollection<TimeTicketViewModel> _timeTickets;

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
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public int TotalElapsedSeconds
        {
            get { return _elapsedSecondsCurrentRun + _elapsedSecondsPreviousRun; }
        }

        public bool CanPause => _stopwatch != null && _stopwatch.IsRunning;
        private ICommand _pauseCommand;
        public ICommand PauseCommand => _pauseCommand ?? (_pauseCommand = new CommandHandler(PauseAction, () => CanPause));

        public bool CanRun => _stopwatch != null && !_stopwatch.IsRunning;
        private ICommand _runCommand;
        public ICommand RunCommand => _runCommand ?? (_runCommand = new CommandHandler(RunAction, () => CanRun));

        public bool CanEdit => true; // always
        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ?? (_editCommand = new CommandHandler(EditAction, () => CanEdit));

        public TimeTicketViewModel(ObservableCollection<TimeTicketViewModel> timeTickets)
        {
            Id = Guid.NewGuid();
            _timeTickets = timeTickets;
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(OnDispatcherTimerTick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _elapsedSecondsCurrentRun = 0;
            _elapsedSecondsPreviousRun = 0;
            DurationTime = "00:00:00";
            Description = "Edit Description";
        }

        public void Start()
        {
            StopOtherTickets();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _dispatcherTimer.Start();
        }

        public void Stop()
        {
            _elapsedSecondsPreviousRun += _elapsedSecondsCurrentRun;
            _elapsedSecondsCurrentRun = 0;
            _stopwatch.Stop();
            _dispatcherTimer.Stop();
        }

        /// <summary>
        /// If this ticket is started or resumed all othet tickets must be stopped.
        /// </summary>
        private void StopOtherTickets()
        {
            var otherTickets = _timeTickets.Where(a => a.Id != this.Id);
            foreach (var ticket in otherTickets)
                ticket.Stop();
        }

        private void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            _elapsedSecondsCurrentRun = (int) _stopwatch.ElapsedMilliseconds / 1000;
            TimeSpan ts = new TimeSpan(0, 0, 0, this.TotalElapsedSeconds, 0);
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
