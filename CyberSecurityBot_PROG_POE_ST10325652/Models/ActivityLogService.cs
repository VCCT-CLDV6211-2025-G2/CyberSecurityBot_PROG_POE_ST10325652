using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityBot_PROG_POE_ST10325652.Models
{
    //Global class for storing timestamps and making them accessible across all views for displaying them in the activity log
    public static class ActivityLogService
    {
         
        public static ObservableCollection<string> Logs { get; } = new();

        public static void AddEntry(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            Logs.Add($"[{timestamp}] {message}");
        }
    }
}
