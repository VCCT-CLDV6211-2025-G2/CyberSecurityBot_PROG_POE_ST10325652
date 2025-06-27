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
   
        private object? _currentView;
        public object? CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView)); // 💡 This triggers the UI update
            }
        }


        public MainViewModel()
        {
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

            sharedChatVM.OnRequestNavigateToQuiz = () =>
            {
                var quizVM = new QuizViewModel();
                quizVM.OnRequestNavigateToChat = () =>
                {
                    CurrentView = new ChatView { DataContext = sharedChatVM };
                };

                CurrentView = new QuizView { DataContext = quizVM };
            };

            CurrentView = new WelcomeView { DataContext = welcomeVM};
        }




        //// Sentiment Responses
        //private string? _sentimentResponse;
        //public string? SentimentResponse
        //{
        //    get { return _sentimentResponse; }
        //    set { _sentimentResponse = value; OnPropertyChanged(nameof(SentimentResponse)); }
        //}

     
       
    }
}
