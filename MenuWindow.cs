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
        public int SelectedIndex { get; private set; }
        public MenuWindow(string[] menuPoints)
        {
            SelectedIndex = 0;
            MenuPoints = menuPoints;
        }

        public void PrintMenuWindowString()
        {
            Console.Clear();
            for (int i = 0; i<MenuPoints.Length; i++)
            {
                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                Console.WriteLine(MenuPoints[i]);
                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

            }

        }

        public void ChangeSelectedIndex(int change)
        {
            if ((SelectedIndex + change) > (MenuPoints.Length - 1))
            {
                SelectedIndex = 0;
            }
            else if (SelectedIndex + change < 0){
                SelectedIndex = MenuPoints.Length -1;
            }
            else
            {
                SelectedIndex += change;
            }
        }
    }
}
