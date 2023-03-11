using System.Windows;
using System.Windows.Input;
using TimeTickets.HelperClasses;
using WPFToolkit;

namespace TimeTickets
{
    public class TimeEditWindowViewModel :  ViewModelBase
    {
        private Window _window;
        private int _initialTotalElapsedSeconds;

        private string _hourPart;
        public string HourPart
        {
            get { return _hourPart; }
            set
            {
                _hourPart = value;
                OnPropertyChanged();
            }
        }

        private string _minutePart;
        public string MinutePart
        {
            get { return _minutePart; }
            set
            {
                _minutePart = value;
                OnPropertyChanged();
            }
        }

        private string _secondPart;
        public string SecondPart
        {
            get { return _secondPart; }
            set
            {
                _secondPart = value;
                OnPropertyChanged();
            }
        }

        private ICommand _cancelWindowCommand;
        public ICommand CancelWindowCommand => _cancelWindowCommand ?? (_cancelWindowCommand = new CommandHandler(CancelWindowAction, () => true));

        private ICommand _submitWindowCommand;
        public ICommand SubmitWindowCommand => _submitWindowCommand ?? (_submitWindowCommand = new CommandHandler(SubmitWindowAction, () => true));

        public TimeEditWindowViewModel(Window window, int totalElapsedSeconds)
        {
            _window = window;
            _initialTotalElapsedSeconds = totalElapsedSeconds;

            Time time = new Time();
            time.SetTimeFromTotalSeconds(_initialTotalElapsedSeconds);
            HourPart = time.Hours.ToString();
            MinutePart = time.Minutes.ToString();
            SecondPart = time.Seconds.ToString();
        }

        public int GetTotalElapsedSeconds()
        {
            if (int.TryParse(SecondPart, out int seconds) && int.TryParse(MinutePart, out int minutes) && int.TryParse(HourPart, out int hours))
            {
                Time time = new Time(hours, minutes, seconds);
                return time.GetTimeAsTotalSeconds();
            }
            return _initialTotalElapsedSeconds;
        }

        private void CancelWindowAction()
        {
            _window.DialogResult = false;
        }

        private void SubmitWindowAction()
        {
            _window.DialogResult = true;
        }
    }
}
