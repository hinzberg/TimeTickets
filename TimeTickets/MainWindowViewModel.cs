﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using TimeTickets.RecuringTasks;
using TimeTickets.TimeTicket;
using WPFToolkit;

namespace TimeTickets
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Day _currentDay;
        private DispatcherTimer _dispatcherTimer;

        public ObservableCollection<TimeTicketViewModel> TimeTicketVMs { get; set; }
        public TimeTicketViewModel SelectedTimeTicketVM { get; set; }

        public bool CanExecuteNewDay => true; // always
        private ICommand _newDayCommand;
        public ICommand NewDayCommand => _newDayCommand ?? (_newDayCommand = new CommandHandler(NewDayAction, () => CanExecuteNewDay));

        public bool CanExecuteNewTask => true; // always
        private ICommand _newTaskCommand;
        public ICommand NewTaskCommand => _newTaskCommand ?? (_newTaskCommand = new CommandHandler(NewTaskAction, () => CanExecuteNewTask));

        public bool CanExecuteRenameTask => SelectedTimeTicketVM != null;
        private ICommand _renameTaskCommand;
        public ICommand RenameTaskCommand => _renameTaskCommand ?? (_renameTaskCommand = new CommandHandler(RenameAction, () => true));

        public bool CanDeleteTask => SelectedTimeTicketVM != null;
        private ICommand _deleteTaskCommand;
        public ICommand DeleteTaskCommand => _deleteTaskCommand ?? (_deleteTaskCommand = new CommandHandler(DeleteAction, () => true));

        private ICommand _manageRecuringTasksCommand;
        public ICommand ManageRecuringTasksCommand => _manageRecuringTasksCommand ?? (_manageRecuringTasksCommand = new CommandHandler(ManageRecuringTasksAction, () => true));


        private string _totalDurationTime;
        public string TotalDurationTime
        {
            get { return _totalDurationTime; }
            set
            {
                _totalDurationTime = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            _currentDay = new Day();

            RepositoryCollection.Instance.DayRepository.Add(_currentDay);

            this.TimeTicketVMs = new ObservableCollection<TimeTicketViewModel>();
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(OnDispatcherTimerTick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _dispatcherTimer.Start();
        }

        private void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, _currentDay.AllElapsedSeconds, 0);
            TotalDurationTime = ts.ToString(@"hh\:mm\:ss");

            foreach (var timeTicket in TimeTicketVMs)
                timeTicket.UpdateDurationTime();
        }

        private void NewDayAction()
        {
            //ToDo issue 1 : Is this really a new day or the same like the previous day?

            foreach (var timeTicket in TimeTicketVMs)
                timeTicket.Stop();

            TimeTicketVMs.Clear();
        }

        private void NewTaskAction()
        {
            Ticket ticket = new Ticket();
            _currentDay.AddTicket(ticket);
            var ticketVM = new TimeTicketViewModel(ticket);
            ticketVM.StopOtherTicketsAction = StopOtherTicketsAction;
            ticketVM.Start();
            this.TimeTicketVMs.Insert(0, ticketVM);
        }

        private void StopOtherTicketsAction(Ticket ticket)
        {
            _currentDay.StopOtherTickets(ticket);
        }

        private void RenameAction()
        {
            TextEditWindow window = new TextEditWindow("Rename Task", "Enter new task name", SelectedTimeTicketVM.Description);
            if (window.ShowDialog().Value)
            {
                SelectedTimeTicketVM.Description = window.InputText;                
            }
        }

        private void DeleteAction()
        {
            this.SelectedTimeTicketVM.Stop();
            this.TimeTicketVMs.Remove(this.SelectedTimeTicketVM);
        }

        private void ManageRecuringTasksAction()
        {
            RecuringTasksWindow window = new RecuringTasksWindow();
            window.ShowDialog();
        }
    }
}
