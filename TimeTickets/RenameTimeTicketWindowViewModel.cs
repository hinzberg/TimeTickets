using System.Windows.Input;
using WPFToolkit;

namespace TimeTickets
{
    public class RenameTimeTicketWindowViewModel : ViewModelBase
    {
        private RenameTimeTicketWindow _window;
        private TimeTicketViewModel _timeTicket;

        private ICommand _confirmCommand;
        public ICommand ConfirmCommand => _confirmCommand ?? (_confirmCommand = new CommandHandler(ConfirmAction, () => true));

        private ICommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new CommandHandler(CancelAction, () => true));

        private string _taskName;
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }

        public RenameTimeTicketWindowViewModel(TimeTicketViewModel timeTicket, RenameTimeTicketWindow window)
        {
            _window = window;
            _timeTicket = timeTicket;
            TaskName = _timeTicket.Description;
        }

        private void ConfirmAction()
        {
            _timeTicket.Description = TaskName;
            _window.DialogResult = true;
        }

        private void CancelAction()
        {
            _window.DialogResult = false;
        }
    }
}
