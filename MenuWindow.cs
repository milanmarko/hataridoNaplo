using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hataridoNaplo
{
    internal class MenuWindow
    {
        private List<string, int> MenuPoints;
        private int SelectedIndex;
        public MenuWindow(Dictionary<string,int> menuPoints)
        {
            SelectedIndex = 0;
            MenuPoints = menuPoints;
        }

        public void PrintMenuWindowString()
        {
            for (int i = 0; i < MenuPoints.Count; i++)
            {
                if(i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                Console.WriteLine(MenuPoints[i]);
                if(i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
        }

        public void ChangeSelectedIndex(int change)
        {
            SelectedIndex += change;
        }
    }
}
