using System.Windows;

namespace TimeTickets
{
    public partial class TextEditWindow : Window
    {
        private TextEditWindowViewModel _textEditWindowViewModel;

        public TextEditWindow(string windowTitle, string labelText, string inputText)
        {
            InitializeComponent();
            _textEditWindowViewModel = new TextEditWindowViewModel(this, windowTitle, labelText, inputText);
            DataContext = _textEditWindowViewModel;
        }

        public string InputText
        {
            get { return _textEditWindowViewModel.InputText; }            
        }
    }
}
