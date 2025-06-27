using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityBot_PROG_POE_ST10325652.Models
{
    // Represents a task in the Cyber Security Bot application
    public class CyberTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        public string DisplayInfo => $"{Title} - {(IsCompleted ? "Completed" : "Pending")} - Reminder: {(ReminderDate.HasValue ? ReminderDate.Value.ToShortDateString() : "None")}";
    }
}
