using CyberSecurityBot_PROG_POE_ST10325652.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CyberSecurityBot_PROG_POE_ST10325652.ViewModels
{
    //View model for the Activity Log View
    public class ActivityLogViewModel : BaseViewModel
    {
        // Gets the list of actvity log entries from the shared ActivityLogService
        public ObservableCollection<string> LogEntries => ActivityLogService.Logs;

        public Action? OnRequestBackToChat { get; set; }

        public ICommand ReturnToChatCommand => new RelayCommand(() =>
        {
            OnRequestBackToChat?.Invoke();
        });
    }
}
