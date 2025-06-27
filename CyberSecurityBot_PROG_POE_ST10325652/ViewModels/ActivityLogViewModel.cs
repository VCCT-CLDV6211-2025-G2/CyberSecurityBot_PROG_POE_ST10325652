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
    public class ActivityLogViewModel : BaseViewModel
    {
        public ObservableCollection<string> LogEntries => ActivityLogService.Logs;

        public Action? OnRequestBackToChat { get; set; }

        public ICommand ReturnToChatCommand => new RelayCommand(() =>
        {
            OnRequestBackToChat?.Invoke();
        });
    }
}
