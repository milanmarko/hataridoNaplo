using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace hataridoNaplo
{
    internal class CreateTodoWindow
    {
        public ToDo CreateTodo()
        {
            Console.Clear();
            string title, desc, deadline;
            DateTime deadlineAsADate;
            do
            {
                Console.Write("Esemény neve: ");
                title = Console.ReadLine();
            } while (title == "");

            do
            {
                Console.Write("Esemény Leírása: ");
                desc = Console.ReadLine();

            } while (desc == "");

            do
            {
                Console.Write("Esemény határideje (éééé-hh-nn óó:pp): ");
                deadline = Console.ReadLine();
            } while (!DateTime.TryParseExact(deadline, "yyyy-MM-dd HH:mm", null, DateTimeStyles.None, out deadlineAsADate));

            return new ToDo(deadlineAsADate, title, desc);
        }
    }
}
