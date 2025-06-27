using CyberSecurityBot_PROG_POE_ST10325652.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace CyberSecurityBot_PROG_POE_ST10325652.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        public Action? OnRequestNavigateToChat { get; set; }

        public ObservableCollection<QuizQuestion> Questions { get; } = new();
        private int _currentIndex = 0;
        private int _score = 0;

        private string? _feedbackMessage;
        public string? FeedbackMessage
        {
            get => _feedbackMessage;
            set { _feedbackMessage = value; OnPropertyChanged(nameof(FeedbackMessage)); }
        }

        public QuizQuestion? CurrentQuestion => _currentIndex < Questions.Count ? Questions[_currentIndex] : null;

        private int _selectedOptionIndex = -1;
        public int SelectedOptionIndex
        {
            get => _selectedOptionIndex;
            set { _selectedOptionIndex = value; OnPropertyChanged(nameof(SelectedOptionIndex)); }
        }

        public ICommand SubmitAnswerCommand => new RelayCommand(() =>
        {
            if (CurrentQuestion == null) return;

            if (SelectedOptionIndex == CurrentQuestion.CorrectOptionIndex)
            {
                _score++;
                FeedbackMessage = "✅ Correct! " + CurrentQuestion.Explanation;
            }
            else
            {
                FeedbackMessage = "❌ Incorrect. " + CurrentQuestion.Explanation;
            }

            // Advance after short delay
            Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                await Task.Delay(2000);
                _currentIndex++;
                SelectedOptionIndex = -1;
                FeedbackMessage = string.Empty;
                OnPropertyChanged(nameof(CurrentQuestion));

                if (_currentIndex >= Questions.Count)
                {
                    ShowFinalScore();
                }
            });
        });

        public QuizViewModel()
        {
            LoadQuestions();
        }

        private void ShowFinalScore()
        {
            MessageBox.Show($"You scored {_score}/{Questions.Count}. " +
                (_score >= 7 ? "Great job! You're a cybersecurity pro!" : "Keep learning to stay safe online."), "Quiz Completed");
        }

        private void LoadQuestions()
        {
            Questions.Add(new QuizQuestion
            {
                QuestionText = "What is phishing?",
                Options = new List<string> {
                    "A cyberattack pretending to be a trusted contact",
                    "A type of firewall",
                    "A secure protocol",
                    "A way to log in securely"
                },
                CorrectOptionIndex = 0,
                Explanation = "Phishing involves fake emails or messages to trick you into giving away information."
            });

            Questions.Add(new QuizQuestion
            {
                QuestionText = "Which of the following is the most secure password?",
                Options = new List<string> {
                    "12345678",
                    "MyName2020",
                    "qwertyuiop",
                    "T9$k2!bQx@"
                },
                CorrectOptionIndex = 3,
                Explanation = "A secure password should be long, complex, and include letters, numbers, and symbols."
            });

            Questions.Add(new QuizQuestion
            {
                QuestionText = "Which of the following is a good practice for online security?",
                Options = new List<string> {
                    "Clicking all links in promotional emails",
                    "Using the same password for every account",
                    "Enabling two-factor authentication",
                    "Sharing your passwords with friends"
                },
                CorrectOptionIndex = 2,
                Explanation = "Two-factor authentication adds an extra layer of protection to your accounts."
            });

            Questions.Add(new QuizQuestion
            {
                QuestionText = "What should you do if you receive a suspicious email?",
                Options = new List<string> {
                    "Reply and ask for more details",
                    "Click the link to see what it is",
                    "Report it as phishing",
                    "Forward it to your contacts"
                },
                CorrectOptionIndex = 2,
                Explanation = "Never interact with suspicious emails — report them as phishing."
            });

            Questions.Add(new QuizQuestion
            {
                QuestionText = "True or False: Public Wi-Fi is always safe to use without precautions.",
                Options = new List<string> {
                    "True",
                    "False"
                },
                CorrectOptionIndex = 1,
                Explanation = "Public Wi-Fi can be unsafe. Use a VPN and avoid accessing sensitive information."
            });

            Questions.Add(new QuizQuestion
            {
                QuestionText = "Which of the following is NOT a type of malware?",
                Options = new List<string> {
                    "Worm",
                    "Trojan",
                    "Firewall",
                    "Ransomware"
                },
                CorrectOptionIndex = 2,
                Explanation = "A firewall is a security feature, not malware."
            });

            Questions.Add(new QuizQuestion
            {
                QuestionText = "How often should you update your software and apps?",
                Options = new List<string> {
                    "Only when they stop working",
                    "Never, it slows down the device",
                    "As soon as updates are available",
                    "Once a year"
                },
                CorrectOptionIndex = 2,
                Explanation = "Updates often contain important security patches."
            });

            Questions.Add(new QuizQuestion
            {
                QuestionText = "What is social engineering in cybersecurity?",
                Options = new List<string> {
                    "Tricking people into revealing sensitive information",
                    "Using software to hack into a network",
                    "Engineering antivirus tools",
                    "Building social media platforms"
                },
                CorrectOptionIndex = 2,
                Explanation = "Social engineering manipulates people, not systems, to get confidential info."
            });

            Questions.Add(new QuizQuestion
            {
                QuestionText = "What should you do if a website's URL begins with 'http' instead of 'https'?",
                Options = new List<string> {
                    "Ignore it — it's always safe",
                    "Proceed only if you're confident it's safe",
                    "Leave immediately — it's insecure",
                    "Refresh the page"
                },
                CorrectOptionIndex = 2,
                Explanation = "HTTPS encrypts data. HTTP sites are not secure, especially for login or payments."
            });

            Questions.Add(new QuizQuestion
            {
                QuestionText = "What is the purpose of antivirus software?",
                Options = new List<string> {
                    "To update your computer",
                    "To back up your files",
                    "To clean your screen",
                    "To detect and remove malicious software"
                },
                CorrectOptionIndex = 3,
                Explanation = "Antivirus software protects against and removes malware."
            });
        }
    }
}

