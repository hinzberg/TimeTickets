using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToolkit;

namespace TimeTickets
{
    class TimeTicketViewModel
    {
        public string DurationTime { get; set; }
        public string Description { get; set; }

        public bool CanPause => true; // always
        private ICommand _pauseCommand;
        public ICommand PauseCommand => _pauseCommand ?? (_pauseCommand = new CommandHandler(PauseAction, () => CanPause));

        public bool CanRun => true; // always
        private ICommand _runCommand;
        public ICommand RunCommand => _runCommand ?? (_runCommand = new CommandHandler(RunAction, () => CanRun));

        public bool CanDelete => true; // always
        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new CommandHandler(DeleteAction, () => CanDelete));

        public bool CanEdit => true; // always
        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ?? (_editCommand = new CommandHandler(EditAction, () => CanEdit));

        public TimeTicketViewModel()
        {
            this.DurationTime = "00:00:00";
            this.Description = "My new Task";
        }

        private void PauseAction()
        {

        }

        private void RunAction()
        {

        }

        private void EditAction()
        {

        }

        private void DeleteAction()
        {

        }
    }
}
