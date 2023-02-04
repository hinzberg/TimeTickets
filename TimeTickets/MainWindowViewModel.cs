using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToolkit;

namespace TimeTickets
{
    public class MainWindowViewModel : ViewModelBase
    {
        public bool CanExecuteClearAll => true; // always
        private ICommand _clearAllCommand;
        public ICommand ClearAllCommand => _clearAllCommand ?? (_clearAllCommand = new CommandHandler(ClearAllAction, () => CanExecuteClearAll));

        public bool CanExecuteNewTask => true; // always
        private ICommand _newTaskCommand;
        public ICommand NewTaskCommand => _newTaskCommand ?? (_newTaskCommand = new CommandHandler(NewTaskAction, () => CanExecuteNewTask));




        private void ClearAllAction()
        {

        }

        private void NewTaskAction()
        {

        }

    }
}
