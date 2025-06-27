using CyberSecurityBot_PROG_POE_ST10325652.CoreLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CyberSecurityBot_PROG_POE_ST10325652.Models;
using System.Windows;


namespace CyberSecurityBot_PROG_POE_ST10325652.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {

        // Instantiating TopicManager
        private readonly TopicManager _topicManager = new TopicManager();

        // User question
        private string? _userQuestion;
        public string? UserQuestion
        {
            get => _userQuestion;
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
        public ICommand AskQuestionCommand => _askQuestionCommand ??= new RelayCommand(
            execute: async() =>
            {
                // Validation for empty string
                if (string.IsNullOrWhiteSpace(UserQuestion))
                {
                    Messages.Add(new ChatMessage("Please enter a question.", "Bot"));
                    return;
                }

                Messages.Add(new ChatMessage(UserQuestion, "User"));

                // Detect keywords like "add a task"
                if (UserQuestion.ToLower().Contains("add a task") || UserQuestion.ToLower().Contains("create a task"))
                {
                    Messages.Add(new ChatMessage("Sure! Taking you to the Task Assistant now.", "Bot"));
                    await Task.Delay(1000);
                    OnRequestNavigateToTasks?.Invoke();
                    UserQuestion = "";
                    return;
                }

                // Detect keywords like "start quiz"
                if (UserQuestion.ToLower().Contains("start quiz") || UserQuestion.ToLower().Contains("play game"))
                {
                    Messages.Add(new ChatMessage("Sure! Taking you to the quiz now.", "Bot"));
                    await Task.Delay(1000);
                    OnRequestNavigateToQuiz?.Invoke();
                    UserQuestion = "";
                    return;
                }


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

                // Tip detection
                if (UserQuestion.ToLower().Contains("tip") || UserQuestion.ToLower().Contains("advice"))
                {
                    string tip = _topicManager.GetRandomTip(matchedTopic);
                    Messages.Add(new ChatMessage($"💡 Tip: {tip}", "Bot"));
                    UserQuestion = "";
                    return;
                }

                // Standard answer matching
                string standardAnswer = _topicManager.GetStandardAnswer(matchedTopic, UserQuestion);
                if (!string.IsNullOrEmpty(standardAnswer))
                {
                    Messages.Add(new ChatMessage(standardAnswer, "Bot"));
                    UserQuestion = "";
                    return;
                }


            });

        public ObservableCollection<ChatMessage> Messages { get; } = new();


        public void ProcessUserInput(string userInput)
        {
            string response = _topicManager.GetBestResponse(userInput);
            Messages.Add(new ChatMessage(response, "Bot"));
        }

        public Action? OnRequestNavigateToTasks { get; set; }

        public string? NewTaskTitle { get; set; }
        public string? NewTaskDescription { get; set; }
        public DateTime? NewTaskReminder { get; set; }


        private ICommand? _addTaskCommand;
        public ICommand AddTaskCommand => _addTaskCommand ??= new RelayCommand(
        execute: () =>
        {
            if (string.IsNullOrWhiteSpace(NewTaskTitle) || string.IsNullOrWhiteSpace(NewTaskDescription))
            {
                MessageBox.Show("Please enter both a title and description.", "Bot");
                return;
            }

            AddTask(NewTaskTitle, NewTaskDescription, NewTaskReminder);

            // Clear inputs
            NewTaskTitle = "";
            NewTaskDescription = "";
            NewTaskReminder = null;
        });


        public void AddTask(string title, string description, DateTime? reminder = null)
        {
            var task = new CyberTask
            {
                Title = title,
                Description = description,
                ReminderDate = reminder,
                IsCompleted = false
            };

            Tasks.Add(task);
            MessageBox.Show($"Task added: {title}. {(reminder != null ? $"Reminder set for {reminder.Value.ToShortDateString()}." : "")}", "Bot");
        }

        private CyberTask? _selectedTask;
        public CyberTask? SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
        }

        public ICommand MarkTaskCompleteCommand => new RelayCommand(() =>
        {
            if (SelectedTask != null)
            {
                SelectedTask.IsCompleted = true;
                OnPropertyChanged(nameof(Tasks)); // to trigger UI refresh
                MessageBox.Show($"Marked '{SelectedTask.Title}' as completed.", "Bot");
            }
            else
            {
                MessageBox.Show("Please select a task first.", "Bot");
            }
        });

        public ICommand DeleteTaskCommand => new RelayCommand(() =>
        {
            if (SelectedTask != null)
            {
                var title = SelectedTask.Title;
                Tasks.Remove(SelectedTask);
                SelectedTask = null;
                MessageBox.Show($"Task '{title}' has been deleted.", "Bot");
            }
            else
            {
                MessageBox.Show("Please select a task first.", "Bot");
            }
        });

        public Action? OnRequestNavigateToChat { get; set; }

        public ICommand ReturnToChatCommand => new RelayCommand(() =>
        {
            OnRequestNavigateToChat?.Invoke();
        });

        public ObservableCollection<CyberTask> Tasks { get; } = new();


        public Action? OnRequestNavigateToQuiz { get; set; }
      
    }
}
