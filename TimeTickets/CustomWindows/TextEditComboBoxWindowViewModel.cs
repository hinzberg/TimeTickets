using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WPFToolkit;

namespace TimeTickets.CustomWindows
{
    class TextEditComboBoxWindowViewModel : ViewModelBase
    {
        private Window _window;

        private ICommand _cancelWindowCommand;
        public ICommand CancelWindowCommand => _cancelWindowCommand ?? (_cancelWindowCommand = new CommandHandler(CancelWindowAction, () => true));

        private ICommand _submitWindowCommand;
        public ICommand SubmitWindowCommand => _submitWindowCommand ?? (_submitWindowCommand = new CommandHandler(SubmitWindowAction, () => true));

        private string _windowTitle;
        public string Title
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged();
            }
        }

        private string _inputText;
        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        private string _labelText;
        public string LabelText
        {
            get { return _labelText; }
            set
            {
                _labelText = value;
                OnPropertyChanged();
            }
        }

        public List<string> ComboBoxContent { get; set; }

        public TextEditComboBoxWindowViewModel(Window window, string windowTitle, string labelText, string inputText, List<string> comboBoxContent)
        {
            _windowTitle = windowTitle;
            _inputText = inputText;
            _labelText = labelText;
            _window = window;
            ComboBoxContent = comboBoxContent;
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
