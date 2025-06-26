using CyberSecurityBot_PROG_POE_ST10325652.CoreLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;


namespace CyberSecurityBot_PROG_POE_ST10325652.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        // Instantiating TopicManager
        private readonly TopicManager _topicManager = new TopicManager();

        // User question
        private string? _userQuestion;
        public string? UserQuestion
        {
            get =>  _userQuestion; 
            set { _userQuestion = value; OnPropertyChanged(nameof(UserQuestion)); }
        }

        // Current Answer
        private string? _currentAnswer;
        public string? CurrentAnswer
        {
            get { return _currentAnswer; }
            set { _currentAnswer = value; OnPropertyChanged(nameof(CurrentAnswer)); }
        }

        private ICommand? _askQuestionCommand;
        public ICommand AskQuestionCommand =>  _askQuestionCommand ??= new RelayCommand(
            execute: () =>
            {
                // Validation for empty string
                if (string.IsNullOrWhiteSpace(UserQuestion))
                {
                    Messages.Add(new ChatMessage("Please enter a question.", "Bot"));
                    return;
                }

                Messages.Add(new ChatMessage(UserQuestion, "User"));

                var matchedTopic = _topicManager.MatchTopic(UserQuestion);
                if (matchedTopic != null)
                {
                    var response = _topicManager.GetTopicAnswer(matchedTopic, UserQuestion);
                    Messages.Add(new ChatMessage(response, "Bot"));
                }
                else
                {
                    Messages.Add(new ChatMessage("I didn't understand that topic. Try rephrasing.", "Bot"));
                }

                UserQuestion = ""; 
            });

        public ObservableCollection<ChatMessage> Messages { get; } = new();

      
    }
}
