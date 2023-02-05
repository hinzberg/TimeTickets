using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WPFToolkit;

namespace TimeTickets
{
    public class TimeTicketViewModel : ViewModelBase
    {
        private string _durationTime;
        private string _description;
        private DispatcherTimer _dispatcherTimer;
        private DateTime _startTime;
        private long _runSeconds;

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

        public bool CanPause => true; // always
        private ICommand _pauseCommand;
        public ICommand PauseCommand => _pauseCommand ?? (_pauseCommand = new CommandHandler(PauseAction, () => CanPause));

        public bool CanRun => true; // always
        private ICommand _runCommand;
        public ICommand RunCommand => _runCommand ?? (_runCommand = new CommandHandler(RunAction, () => CanRun));

        public bool CanDelete => true; // always
        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new CommandHandler(DeleteAction, () => CanDelete));

        public bool CanEdit => true; // always
        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ?? (_editCommand = new CommandHandler(EditAction, () => CanEdit));

        public TimeTicketViewModel()
        {
            _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(OnDispatcherTimerTick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            DurationTime = "00:00:00";
            Description = "Edit Description";
        }

        public void Start()
        {
            _startTime = DateTime.Now;
            _dispatcherTimer.Start();
        }

        private void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - _startTime;
            DurationTime = ts.ToString(@"hh\:mm\:ss");
        }

        public void Stop()
        {
            _dispatcherTimer.Stop();
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

        private void DeleteAction()
        {
        }
    }
}
