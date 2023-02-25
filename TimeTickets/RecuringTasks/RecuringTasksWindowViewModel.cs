using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFToolkit;

namespace TimeTickets.RecuringTasks
{
    public class RecuringTasksWindowViewModel
    {
        private readonly RecuringTasksWindow _window;
        public ObservableCollection<RecuringTaskViewModel> RecuringTaskVMs { get; set; }
        public RecuringTask SelectedItem { get; set; }

        private ICommand _closeWindowCommand;
        public ICommand CloseWindowCommand => _closeWindowCommand ?? (_closeWindowCommand = new CommandHandler(CloseWindowAction, () => true));

        private ICommand _newRecuringCommand;
        public ICommand NewRecuringCommand => _newRecuringCommand ?? (_newRecuringCommand = new CommandHandler(NewRecuringAction, () => true));

        public RecuringTasksWindowViewModel(RecuringTasksWindow window)
        {
            _window = window;
            RecuringTaskVMs = new ObservableCollection<RecuringTaskViewModel>();

            foreach (var recuringTask in RepositoryCollection.Instance.RecuringTasksRepository.RecuringTasks)
                RecuringTaskVMs.Add(CreateRecuringTaskViewModel(recuringTask));
        }

        private RecuringTaskViewModel CreateRecuringTaskViewModel(RecuringTask recuringTask)
        {
            var taskVM = new RecuringTaskViewModel(recuringTask);
            taskVM.DeleteAction = DeleteAction;
            taskVM.EditAction = EditAction;
            return taskVM;
        }

        private void DeleteAction(RecuringTaskViewModel taskVM)
        {
            RecuringTaskVMs.Remove(taskVM);
            RepositoryCollection.Instance.RecuringTasksRepository.RecuringTasks.Remove(taskVM.GetModel());
        }

        private void EditAction(RecuringTaskViewModel taskVM)
        {

        }

        private void NewRecuringAction()
        {
            TextEditWindow textEditWindow = new TextEditWindow(@"New recuring task", @"Enter new task name", "Default");
            if (textEditWindow.ShowDialog().Value) ;
            {
                var recuringTask = new RecuringTask();
                recuringTask.Description = textEditWindow.InputText;
                RepositoryCollection.Instance.RecuringTasksRepository.RecuringTasks.Add(recuringTask);
                RecuringTaskVMs.Add(CreateRecuringTaskViewModel(recuringTask));
            }
        }

        private void CloseWindowAction()
        {
            _window.DialogResult = false;
        }
    }
}
