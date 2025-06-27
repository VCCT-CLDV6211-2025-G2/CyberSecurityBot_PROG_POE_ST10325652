using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityBot_PROG_POE_ST10325652.Models
{
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
