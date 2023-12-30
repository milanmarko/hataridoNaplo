using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hataridoNaplo
{
    internal class ToDo
    {
        public DateTime Deadline { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        private ToDo(string title, string desc)
        {
            Title = title;
            Description = desc;
        }
        public ToDo(DateTime deadlineAsADate, string title, string desc)
            : this(title, desc)
        {
            Deadline = deadlineAsADate;
        }
        public ToDo(string dateStr, string title, string timeStr, string desc)
            : this(title, desc)
        {
            Deadline = DateTime.ParseExact($"{dateStr} {timeStr}", "yyyy-MM-dd HH:mm", null);
        }

        public string[] GetDetails()
        {
            return new string[] { $"Esemény neve: {Title}", $"Esemény határideje: {Deadline}", $"Esemény Leírása: {Description}" };
        }
    }
}
