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
        public ToDo(string dateStr, string title, string timeStr, string desc)
        {
            Deadline = DateTime.ParseExact($"{dateStr} {timeStr}", "yyyy-MM-dd HH:mm", null);
            Title = title;
            Description = desc;
        }
    }
}
