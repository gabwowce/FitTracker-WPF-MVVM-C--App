using FitTracker.Models;
using FitTracker.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace FitTracker.ViewModels
{
    public class DashboardViewModel: INotifyPropertyChanged
    {
        
        private int UserID;
        private string _task;
        private bool _isCompleted;


        public string Task
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged(nameof(Task));
            }
        }

        public bool isCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(isCompleted));
            }
        }

        private ObservableCollection<UserToDoListTasks> _toDoListTasks;
        public ObservableCollection<UserToDoListTasks> ToDoListTasks
        {
            get => _toDoListTasks;
            set
            {
                _toDoListTasks = value;
                OnPropertyChanged(nameof(ToDoListTasks));
            }
        }
        public UserToDoListTasks CurrentUserToDoListTask { get; private set; }
        public MenuBarViewModel MenuBar { get; private set; }

        private UserToDoListTasksRepository repository;

        public DashboardViewModel(MenuBarViewModel menuBarViewModel, int userID)
        {
            this.UserID = userID;
            MenuBar = menuBarViewModel;
            this.ToDoListTasks = new ObservableCollection<UserToDoListTasks>();
            this.repository = new UserToDoListTasksRepository(ConfigurationManager.ConnectionStrings["MyConnectionToDB"].ConnectionString);
        }
        public async Task InitializeAsync()
        {
            await LoadToDoListTasks();
        }

        private async Task LoadToDoListTasks()
        {
            
            var toDoListTasks = await repository.GetToDoListTasksAsync(UserSession.UserId);
            ToDoListTasks = new ObservableCollection<UserToDoListTasks>(toDoListTasks);
            OnPropertyChanged(nameof(toDoListTasks));
        }

        public void DeleteToDoListTask(UserToDoListTasks task)
        {
            if (task != null)
            {
                repository.DeleteToDoTask(task.TaskID);
                ToDoListTasks.Remove(task);
            }
        }

        public void SaveRecord()
        {

            if (CurrentUserToDoListTask == null)
            {
                CurrentUserToDoListTask = new UserToDoListTasks();
            }

            CurrentUserToDoListTask.UserID = this.UserID;
            CurrentUserToDoListTask.Task = this.Task;
            CurrentUserToDoListTask.isCompleted = this.isCompleted;

            repository.AddTask(CurrentUserToDoListTask, this.UserID);

            OnPropertyChanged(nameof(CurrentUserToDoListTask));
            ToDoListTasks.Add(CurrentUserToDoListTask);  
        }

        public void UpdateTask(UserToDoListTasks task)
        {
            if (task != null)
            {
                repository.UpdateTask(task);
                OnPropertyChanged(nameof(ToDoListTasks));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
