using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hataridoNaplo
{
    internal class App
    {
        private MenuWindow MainMenu;
        private MenuWindow CurrentlyVisibleWindow;
        private bool isAppRunning;
        public App()
        {
            MainMenu = new MenuWindow(new Dictionary<string, int> { { "Mai teendőim", 1 }, { "Új teendő hozzáadása", 2 }, { "Teendők megtekintése dátum alapján", 3 }, { "Beállítások", 4 }, { "Kilépés", 5 } });
            isAppRunning = true;
            CurrentlyVisibleWindow = MainMenu;
        }
        private int HandleKeyboard()
        {
            do
            {
                ConsoleKey pressedButton = Console.ReadKey(true).Key;
                switch (pressedButton)
                {
                    case ConsoleKey.DownArrow:
                        return 1;
                    case ConsoleKey.UpArrow:
                        return -1;
                    case ConsoleKey.Enter:
                        return 0;
                    default:
                        throw new Exception();
                }
            } while (true);
        }
        public void RunApp()
        {
            while (isAppRunning)
            {
                CurrentlyVisibleWindow.PrintMenuWindowString();
                int keyboardReturn = HandleKeyboard();
                if(keyboardReturn == 1 || keyboardReturn == 0)
                {
                    CurrentlyVisibleWindow.ChangeSelectedIndex(keyboardReturn);
                }
                else
                {
                    C
                }
            }
        }
    }
}
