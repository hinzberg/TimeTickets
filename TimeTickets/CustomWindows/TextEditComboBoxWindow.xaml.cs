using System.Collections.Generic;
using System.Windows;

namespace TimeTickets.CustomWindows
{
    public partial class TextEditComboBoxWindow : Window
    {
        private TextEditComboBoxWindowViewModel _textEditWindowViewModel;

        public TextEditComboBoxWindow(string windowTitle, string labelText, string inputText , List<string> comboBoxContent)
        {
            InitializeComponent();
            _textEditWindowViewModel = new TextEditComboBoxWindowViewModel(this, windowTitle, labelText, inputText, comboBoxContent);
            DataContext = _textEditWindowViewModel;
        }

        public string InputText
        {
            get { return _textEditWindowViewModel.InputText; }
        }
    }
}
