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
        public int SelectedIndex { get; private set; }
        public MenuWindow(string[] menuPoints)
        {
            SelectedIndex = 0;
            MenuPoints = menuPoints;
            pointsCount = MenuPoints.Length;
        }
        public MenuWindow(string[] menuPoints, ToDo[] ToDos)
        {
            SelectedIndex = 0;
            MenuPoints = menuPoints;
            ToDosIfToDo = ToDos;
            pointsCount = menuPoints.Length;
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
            PrintSomething(MenuPoints);
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
        public void ShowToDoDetails(int index)
        {
            pointsCount = 3;
            PrintSomething(ToDosIfToDo[index].GetDetails());
        }
    }
}
