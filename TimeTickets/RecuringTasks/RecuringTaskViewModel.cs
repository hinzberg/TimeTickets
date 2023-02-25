using System;
using System.Windows.Input;
using TimeTickets.Interfaces;
using WPFToolkit;

namespace TimeTickets.RecuringTasks
{
    public class RecuringTaskViewModel : ViewModelBase, IViewModel<RecuringTask>
    {
        RecuringTask _recuringTask;

        public string Description
        {
            get { return _recuringTask.Description; }
            set
            {
                _recuringTask.Description = value;
                OnPropertyChanged();
            }
        }

        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ?? (_editCommand = new CommandHandler(EditCommandAction, () => true));

        private ICommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new CommandHandler(DeleteCommandAction, () => true));

        public Action<RecuringTaskViewModel> DeleteAction { get; set; }
        public Action<RecuringTaskViewModel> EditAction { get; set; }

        public RecuringTaskViewModel(RecuringTask recuringTask)
        {
            _recuringTask = recuringTask;
        }

        private void DeleteCommandAction()
        {
            DeleteAction?.Invoke(this);
        }

        private void EditCommandAction()
        {
            EditAction?.Invoke(this);
        }

        public RecuringTask GetModel()
        {
            return _recuringTask;
        }
    }
}
