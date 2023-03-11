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
    public partial class TimeEditWindow : Window
    {
        private TimeEditWindowViewModel _viewModel;

        public TimeEditWindow(int totalElapsedSeconds)
        {
            InitializeComponent();
            _viewModel = new TimeEditWindowViewModel(this, totalElapsedSeconds);
            DataContext = _viewModel;
        }
        public int GetTotalElapsedSeconds()
        {
            return _viewModel.GetTotalElapsedSeconds();
        }
    }
}
