using System.Windows;

namespace TimeTickets
{
    public partial class RenameTimeTicketWindow : Window
    {
        public RenameTimeTicketWindow(TimeTicketViewModel timeTicket)
        {
            InitializeComponent();
            this.DataContext = new RenameTimeTicketWindowViewModel(timeTicket, this);
        }
    }
}
