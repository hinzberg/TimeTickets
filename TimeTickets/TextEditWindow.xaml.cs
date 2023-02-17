using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
