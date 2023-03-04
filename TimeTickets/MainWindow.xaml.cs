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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeTickets
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RepositoryCollection.Instance.Load();
            this.DataContext = new MainWindowViewModel();            
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            RepositoryCollection.Instance.Save();
        }
    }
}
