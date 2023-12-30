using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hataridoNaplo
{
    internal class MenuWindow
    {
        private string[] MenuPoints;
        private ToDo[] ToDosIfToDo;
        private int pointsCount;
        public bool ShowingDetails { get; private set; }
        public int SelectedIndex { get; private set; }
        public ToDo DetailedToDo { get; private set; }
        public MenuWindow(string[] menuPoints)
        {
            ShowingDetails = false;
            SelectedIndex = 0;
            MenuPoints = menuPoints;
            pointsCount = MenuPoints.Length;
        }
        public MenuWindow(string[] menuPoints, ToDo[] ToDos)
            : this(menuPoints)
        {
            ToDosIfToDo = ToDos;
        }

        private void PrintSomething(string[] strings)
        {
            Console.Clear();
            for (int i = 0; i < strings.Length; i++)
            {
                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                Console.WriteLine(strings[i]);
                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

            }
        }
        public void PrintMenuWindowString()
        {
            pointsCount = MenuPoints.Length;
            ShowingDetails = false;
            if (MenuPoints.Length == 0)
            {
                Console.Clear();
                Console.WriteLine("Nincsenek teendői!");
            }
            else
            {
                PrintSomething(MenuPoints);
            }
        }
        

        public void ChangeSelectedIndex(int change)
        {
            if ((SelectedIndex + change) > (pointsCount - 1))
            {
                SelectedIndex = 0;
            }
            else if (SelectedIndex + change < 0){
                SelectedIndex = pointsCount -1;
            }
            else
            {
                SelectedIndex += change;
            }
        }
        public bool isThereAnyTodo()
        {
            if (ToDosIfToDo.Length == 0)
            {
                return false;
            }
            return true;
        }
        public void ShowToDoDetails(int index)
        {
            ShowingDetails = true;
            DetailedToDo = ToDosIfToDo[index];
            pointsCount = 4;
            PrintSomething(ToDosIfToDo[index].GetDetails().Concat(new string[]{ "[Teendő törlése]"}).ToArray());
        }
    }
}
