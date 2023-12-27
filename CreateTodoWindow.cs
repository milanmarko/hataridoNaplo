using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hataridoNaplo
{
    internal class CreateTodoWindow
    {
        public ToDo CreateTodo()
        {
            Console.Clear();
            Console.Write("Esemény neve: ");
            string title = Console.ReadLine();
            Console.Write("Esemény Leírása: ");
            string desc = Console.ReadLine();
            Console.Write("Esemény határideje (éééé-hh-nn óó:pp): ");
            string deadline = Console.ReadLine();
            string[] splittedDeadline = deadline.Split(' ');
            return new ToDo(splittedDeadline[0], title, splittedDeadline[1], desc);
        }
    }
}
