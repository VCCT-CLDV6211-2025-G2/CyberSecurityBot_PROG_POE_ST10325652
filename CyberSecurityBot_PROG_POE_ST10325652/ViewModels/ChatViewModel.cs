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

        // Property for user question. Stores the current user input
        private string? _userQuestion;
        public string? UserQuestion
        {
            get => _userQuestion;
            set { _userQuestion = value; OnPropertyChanged(nameof(UserQuestion)); }
        }

        // Collection of chat messages. Automatically updates the UI when new messages are added
        public ObservableCollection<ChatMessage> Messages { get; } = new();

        // Current Answer
        private string? _currentAnswer;
        public string? CurrentAnswer
        {
            get { return _currentAnswer; }
            set { _currentAnswer = value; OnPropertyChanged(nameof(CurrentAnswer)); }
        }

        // Selected Task
        private CyberTask? _selectedTask;
        public CyberTask? SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
        }

       
        #region ICommand for asking questions
        //Handlers user input, checks for keywords, and returns standard answers or navigates to other views.
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

                Messages.Add(new ChatMessage(UserQuestion, "User")); // Log user question in chat
                ActivityLogService.AddEntry($"User asked: {UserQuestion}"); // Log user question

                // Dectect keywords like "show log" to open the activity log view
                if (UserQuestion.ToLower().Contains("show log") || UserQuestion.ToLower().Contains("activity"))
                {
                    Messages.Add(new ChatMessage("Opening activity log...", "Bot"));
                    ActivityLogService.AddEntry("Navigated to Activity Log");
                    await Task.Delay(3000);
                    OnRequestNavigateToActivityLog?.Invoke();
                    UserQuestion = "";
                    return;
                }

                // Detect keywords like "add a task" to open the task assistant view
                if (UserQuestion.ToLower().Contains("add a task") || UserQuestion.ToLower().Contains("create a task"))
                {
                    Messages.Add(new ChatMessage("Sure! Taking you to the Task Assistant now.", "Bot"));
                    ActivityLogService.AddEntry("Navigated to Task Assistant");
                    await Task.Delay(3000);
                    OnRequestNavigateToTasks?.Invoke();
                    UserQuestion = "";
                    return;
                }

                // Detect keywords like "start quiz" to open the quiz view
                if (UserQuestion.ToLower().Contains("start quiz") || UserQuestion.ToLower().Contains("play game"))
                {
                    Messages.Add(new ChatMessage("Sure! Taking you to the quiz now.", "Bot"));
                    ActivityLogService.AddEntry("Navigated to Quiz");
                    await Task.Delay(3000);
                    OnRequestNavigateToQuiz?.Invoke();
                    UserQuestion = "";
                    return;
                }

                // Match the user question to a topic from TopicManager class
                var matchedTopic = _topicManager.MatchTopic(UserQuestion);
                if (matchedTopic != null)
                {
                    // Standard answer matching from method from TopicManager class
                    string standardAnswer = _topicManager.GetStandardAnswer(matchedTopic, UserQuestion);
                    if (!string.IsNullOrEmpty(standardAnswer))
                    {
                        Messages.Add(new ChatMessage(standardAnswer, "Bot"));
                        ActivityLogService.AddEntry($"Bot replied: {standardAnswer}");
                        UserQuestion = "";
                        return;
                    }

                    // Dectect keywords like "tip" to call the GetRandomTip method from TopicManager class
                    if (UserQuestion.ToLower().Contains("tip") || UserQuestion.ToLower().Contains("advice"))
                    {
                        string tip = _topicManager.GetRandomTip(matchedTopic);
                        Messages.Add(new ChatMessage($"💡 Tip: {tip}", "Bot"));
                        ActivityLogService.AddEntry($"Bot replied: {tip}");
                        UserQuestion = "";
                        return;
                    }
                    // Returns the answer for the matched topic using the GetTopicAnswer method from TopicManager class
                    var response = _topicManager.GetTopicAnswer(matchedTopic, UserQuestion);
                    Messages.Add(new ChatMessage(response, "Bot"));
                    ActivityLogService.AddEntry($"Bot replied: {response}");
                }
                // If no topic matched, return a default message
                else
                {
                    Messages.Add(new ChatMessage("I didn't understand that topic. Try rephrasing.", "Bot"));
                    ActivityLogService.AddEntry("Unmatched topic.");
                }

                UserQuestion = "";

            });
        #endregion


        #region ICommand methods for task management
        // Properties for adding a new task
        public string? NewTaskTitle { get; set; }
        public string? NewTaskDescription { get; set; }
        public DateTime? NewTaskReminder { get; set; }
        public ObservableCollection<CyberTask> Tasks { get; } = new();

        // Command to add a new task
        private ICommand? _addTaskCommand;
        public ICommand AddTaskCommand => _addTaskCommand ??= new RelayCommand(
        execute: () =>
        {
            if (string.IsNullOrWhiteSpace(NewTaskTitle) || string.IsNullOrWhiteSpace(NewTaskDescription))
            {
                MessageBox.Show("Please enter both a title and description.", "Bot");
                return;
            }

            AddTask(NewTaskTitle, NewTaskDescription, NewTaskReminder); // Add the task to the list

            // Clear inputs
            NewTaskTitle = "";
            NewTaskDescription = "";
            NewTaskReminder = null;
        });

        // Method to add a task to the list
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

        
        // Command to mark a task as completed
        public ICommand MarkTaskCompleteCommand => new RelayCommand(() =>
        {
            if (SelectedTask != null)
            {
                SelectedTask.IsCompleted = true;
                OnPropertyChanged(nameof(Tasks)); // to trigger UI refresh
                MessageBox.Show($"Marked '{SelectedTask.Title}' as completed.", "Bot"); //Pop up message 
            }
            else
            {
                MessageBox.Show("Please select a task first.", "Bot");
            }
        });

        // Command to delete a task
        public ICommand DeleteTaskCommand => new RelayCommand(() =>
        {
            if (SelectedTask != null)
            {
                var title = SelectedTask.Title;
                Tasks.Remove(SelectedTask);
                SelectedTask = null;
                MessageBox.Show($"Task '{title}' has been deleted.", "Bot"); //Pop up message
            }
            else
            {
                MessageBox.Show("Please select a task first.", "Bot");
            }
        });
        #endregion

        //Natural language processing for user input
        //Calls the GetBestResponse method from TopicManager class to process user input and return a response.
        public void ProcessUserInput(string userInput)
        {
            string response = _topicManager.GetBestResponse(userInput);
            Messages.Add(new ChatMessage(response, "Bot"));
        }

        //Navigation Commands
        public ICommand ReturnToChatCommand => new RelayCommand(() =>
        {
            OnRequestNavigateToChat?.Invoke();
        });

        public Action? OnRequestNavigateToChat { get; set; }

        public Action? OnRequestNavigateToTasks { get; set; }

        public Action? OnRequestNavigateToQuiz { get; set; }

        public Action? OnRequestNavigateToActivityLog { get; set; }


    }
}

//https://www.w3schools.com/cs/index.php