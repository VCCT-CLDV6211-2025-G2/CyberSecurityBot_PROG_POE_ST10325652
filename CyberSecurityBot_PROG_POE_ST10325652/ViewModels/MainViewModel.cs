using CyberSecurityBot_PROG_POE_ST10325652.CoreLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CyberSecurityBot_PROG_POE_ST10325652;

namespace CyberSecurityBot_PROG_POE_ST10325652.ViewModels
{
    #region ICommand implementation that takes delefates for Execute and CanExecute methods.
    // Used to connect UI buttons to methods in ViewModels using ICommand.
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) =>
            _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) =>
            _execute();

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
    #endregion

    public class MainViewModel : BaseViewModel
    {
        // This is the main view model that manages navigation between different views in the application.

        // It holds the current view and provides methods to switch between views based on user actions.
        private object? _currentView;
        public object? CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView)); // This triggers the UI update
            }
        }

        // Constructor for MainViewModel
        public MainViewModel()
        {
            // Initialize the view models for different views
            var welcomeVM = new WelcomeViewModel();
            var sharedChatVM = new ChatViewModel();

            // Hook up navigation from ChatViewModel to TaskView
            sharedChatVM.OnRequestNavigateToTasks = () =>
            {
                CurrentView = new TaskView { DataContext = sharedChatVM };
            };

            // Hook up navigation from WelcomeViewModel to ChatView
            welcomeVM.OnRequestNavigateToChat = () =>
            {
                CurrentView = new ChatView { DataContext = sharedChatVM }; 
            };

            // Hook up navigation from TaskView to ChatView
            sharedChatVM.OnRequestNavigateToChat = () =>
            {
                CurrentView = new ChatView { DataContext = sharedChatVM };
            };

            // Hook up navigation from ChatViewModel to QuizView and ActivityLogView
            sharedChatVM.OnRequestNavigateToQuiz = () =>
            {
                var quizVM = new QuizViewModel();
                quizVM.OnRequestNavigateToChat = () =>
                {
                    CurrentView = new ChatView { DataContext = sharedChatVM };
                };

                CurrentView = new QuizView { DataContext = quizVM };
            };

            // Hook up navigation from ChatViewModel to ActivityLogView
            sharedChatVM.OnRequestNavigateToActivityLog = () =>
            {
                var logVM = new ActivityLogViewModel();
                logVM.OnRequestBackToChat = () =>
                {
                    CurrentView = new ChatView { DataContext = sharedChatVM };
                };

                CurrentView = new ActivityLogView { DataContext = logVM };
            };

            CurrentView = new WelcomeView { DataContext = welcomeVM};
        }

       
    }
}

//https://www.w3schools.com/cs/index.php
