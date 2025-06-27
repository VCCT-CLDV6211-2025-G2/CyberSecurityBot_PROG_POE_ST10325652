using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using CyberSecurityBot_PROG_POE_ST10325652.CoreLogic;

namespace CyberSecurityBot_PROG_POE_ST10325652.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {

        // Greeting text
        private string? _greetingText;
        public string? GreetingText
        {
            get { return _greetingText; }
            set
            {
                _greetingText = value; 
                OnPropertyChanged(nameof(GreetingText));
            }
        }

        // ASCII Art for the lock
        public string _asciiArtLock = @"
            .-""-.
           / .--. \
          / /    \ \
          | |    | |
          | |.-""-. |
         ///`.::::.`\
        ||| ::/  \:: ;
        ||; ::\__/:: ;
         \\\ '::::' /
          `=':-..-'`
            ";

        public string AsciiArtLock
        {
            get { return _asciiArtLock; }
            set { _asciiArtLock = value; OnPropertyChanged(nameof(AsciiArtLock)); }
        }

        // ASCII Art for the welcome message
        public string _asciiArtMessage = @"  ___          ___                       ___ 
     /\  \        /\__\  ___                /\__\        /\  \        /\  \        /\__\    
    _\:\  \      /:/ _/_:\  \              /:/  /       /::\  \      |::\  \      /:/ _/_   
   /\ \:\  \    /:/ /\__\:\  \            /:/  /       /:/\:\  \     |:|:\  \    /:/ /\__\  
  _\:\ \:\  \  /:/ /:/ _/\:\  \     ___  /:/  /  ___  /:/  \:\  \  __|:|\:\  \  /:/ /:/ _/_ 
 /\ \:\ \:\__\/:/_/:/ /\__\:\  \   /\__\/:/__/  /\__\/:/__/ \:\__\/::::|_\:\__\/:/_/:/ /\__\
 \:\ \:\/:/  /\:\/:/ /:/  /\:\  \ /:/  /\:\  \ /:/  /\:\  \ /:/  /\:\~~\  \/__/\:\/:/ /:/  /
  \:\ \::/  /  \::/_/:/  /  \:\  /:/  /  \:\  /:/  /  \:\  /:/  /  \:\  \       \::/_/:/  / 
   \:\/:/  /    \:\/:/  /    \:\/:/  /    \:\/:/  /    \:\/:/  /    \:\  \       \:\/:/  /  
    \::/  /      \::/  /      \::/  /      \::/  /      \::/  /      \:\__\       \::/  /   
     \/__/        \/__/        \/__/        \/__/        \/__/        \/__/        \/__/    ";

        public string AsciiArtMessage
        {
            get { return _asciiArtMessage; }
            set { _asciiArtMessage = value; OnPropertyChanged(nameof(AsciiArtMessage)); }
        }

        private readonly VoiceGreeting _voiceGreeting = new VoiceGreeting();

        private ICommand? _showWelcomeCommand;
        public ICommand ShowWelcomeCommand =>
            _showWelcomeCommand ??= new RelayCommand(async () =>
            {
                GreetingText = "";
                string full = "Hello! Welcome to the CyberSecurity awareness bot. I am here to help you stay safe online!";

                var voiceTask = _voiceGreeting.PlayAudioAsync();

                foreach (char c in full)
                {
                    GreetingText += c;
                    await Task.Delay(50); // Simulate typing effect
                }
            });

        public WelcomeViewModel()
        {
            ShowWelcomeCommand.Execute(null);
        }

        // Name input
        private string? _nameInput;
        public string? NameInput
        {
            get => _nameInput;
            set { _nameInput = value; OnPropertyChanged(nameof(NameInput)); }
        }

        private string? _nameError;
        public string? NameError
        {
            get => _nameError;
            set { _nameError = value; OnPropertyChanged(nameof(NameError)); }
        }

        private string? _personalGreeting;
        public string? PersonalGreeting
        {
            get => _personalGreeting;
            set { _personalGreeting = value; OnPropertyChanged(nameof(PersonalGreeting)); }
        }

        public Action? OnRequestNavigateToChat { get; set; }

        public ICommand SubmitNameCommand => new RelayCommand(async() =>
        {
            if (string.IsNullOrWhiteSpace(NameInput))
            {
                NameError = "Please enter a valid name";
                return;
            }

            NameError = null;
            PersonalGreeting = $"Welcome, {NameInput}!";

            await Task.Delay(1000); // short pause before switching views

            // Call a method in MainViewModel to go to ChatView 
            OnRequestNavigateToChat?.Invoke();

        });


    }
}
