using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFToolkit;

namespace TimeTickets.RecuringTasks
{
    public class RecuringTasksWindowViewModel
    {
        private readonly RecuringTasksWindow _window;
        public ObservableCollection<RecuringTask> RecuringTasks { get; set; }
        public RecuringTask SelectedItem { get; set; }

        private ICommand _cancelWindowCommand;
        public ICommand CancelWindowCommand => _cancelWindowCommand ?? (_cancelWindowCommand = new CommandHandler(CancelWindowAction, () => true));

        private ICommand _submitWindowCommand;
        public ICommand SubmitWindowCommand => _submitWindowCommand ?? (_submitWindowCommand = new CommandHandler(SubmitWindowAction, () => true));

        private ICommand _newRecuringCommand;
        public ICommand NewRecuringCommand => _newRecuringCommand ?? (_newRecuringCommand = new CommandHandler(NewRecuringAction, () => true));


        public RecuringTasksWindowViewModel(RecuringTasksWindow window)
        {
            _window = window;
            RecuringTasks = RepositoryCollection.Instance.RecuringTasksRepository.RecuringTasks;
        }

        private void NewRecuringAction()
        {
            TextEditWindow textEditWindow = new TextEditWindow(@"New recuring task",  @"Enter new task name", "Default");
            if(textEditWindow.ShowDialog().Value);
            {
                var task = new RecuringTask();
                task.Description = textEditWindow.InputText;
                RecuringTasks.Add(task);
            }
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
