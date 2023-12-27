using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hataridoNaplo
{
    internal class App
    {
        private MenuWindow VisibleWindow;
        private MenuWindow PreviousWindow;
        private MenuWindow MainMenu;
        private MenuWindow TodaysToDos;
        private CreateTodoWindow CreateTodo;
        private bool isAppRunning;
        private List<ToDo> ToDoList;
        public App()
        {
            isAppRunning = true;
            ToDoList = GetAllToDos();
            MainMenu = new MenuWindow(new string[] { "Mai teendőim", "Új teendő hozzáadása", "Teendők megtekintése dátum alapján", "Beállítások", "Kilépés" });
            TodaysToDos = new MenuWindow(ToDoList.Where(x => x.Deadline.Date == DateTime.Today).Select(x => x.Title).ToArray(), ToDoList.ToArray());
            CreateTodo = new CreateTodoWindow();
            VisibleWindow = MainMenu;
            //Console.WriteLine(ToDoList.Count);
        }
        private List<ToDo> GetAllToDos()
        {
            List<ToDo> futureReturn = new List<ToDo>();
            StreamReader sr = new StreamReader("ToDos.txt",Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                string[] splittedLine = sr.ReadLine().Split(';');
                for (int i = 1; i < splittedLine.Length; i++)
                {
                    Console.WriteLine(splittedLine[0]);
                    string[] toDoDetails = splittedLine[i].Split('\\');
                    Console.WriteLine(toDoDetails[0]);
                    futureReturn.Add(new ToDo(splittedLine[0], toDoDetails[0], toDoDetails[1], toDoDetails[2]));
                }
            }
            sr.Close();
            return futureReturn;
        }
        private void Save()
        {
            StreamWriter sw = new StreamWriter("ToDos.txt");
            ToDoList = ToDoList.OrderBy(x => x.Deadline.Ticks).ToList();
            List<DateTime> datesDone = new List<DateTime>();
            foreach (var ToDo in ToDoList)
            {
                DateTime date = ToDo.Deadline;
                if (!datesDone.Contains(date.Date))
                {
                    List<ToDo> toDosOnDate = ToDoList.Where(x => x.Deadline.Date == date.Date).ToList();
                    sw.WriteLine($"{date.Year}-{date.Month}-{date.Day};{string.Join(';', toDosOnDate.Select(x => x.Title + '\\' + (x.Deadline.Hour < 10 ? '0' : string.Empty)+ x.Deadline.Hour  + ':' + (x.Deadline.Minute < 10 ? '0' : string.Empty) + x.Deadline.Minute + '\\' + x.Description).ToList())}");
                    datesDone.Add(date.Date);
                }
            }
            sw.Close();
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
                    case ConsoleKey.LeftArrow:
                        return 2;
                    default:
                        return -2;
                }
            } while (true);
        }
        private void OpenWindow(int WindowGlobalIndex)
        {
            PreviousWindow = VisibleWindow;
            if (VisibleWindow == MainMenu)
            {
                switch (WindowGlobalIndex)
                {
                    case 0:
                        VisibleWindow = TodaysToDos;
                        break;
                    case 1:
                        ToDoList.Add(CreateTodo.CreateTodo());
                        Save();
                        List<ToDo> TodaysToDosList = ToDoList.Where(x => x.Deadline.Date == DateTime.Today).ToList();
                        TodaysToDos = new MenuWindow(TodaysToDosList.Select(x => x.Title).ToArray(), TodaysToDosList.ToArray());
                        break;
                    case 4:
                        Save();
                        throw new Exception();
                    default: break;
                }
            }
            else if (VisibleWindow == TodaysToDos)
            {
                int originalIndex = VisibleWindow.SelectedIndex;
                PreviousWindow = MainMenu;
                while (VisibleWindow == TodaysToDos)
                {
                    TodaysToDos.ShowToDoDetails(originalIndex);
                    Route();
                }
            }
        }
        private void Route()
        {
            int keyboardReturn = HandleKeyboard();
            if (keyboardReturn == 1 || keyboardReturn == -1)
            {
                VisibleWindow.ChangeSelectedIndex(keyboardReturn);
            }
            else if (keyboardReturn == 2)
            {
                VisibleWindow = PreviousWindow;
            }
            else if (VisibleWindow == MainMenu || VisibleWindow == TodaysToDos)
            {
                OpenWindow(VisibleWindow.SelectedIndex);
            }
        }
        public void RunApp()
        {
            try
            {
                while (isAppRunning)
                {
                    //Teljesen át kell variálni
                    //Ablakok váltása TODO

                    VisibleWindow.PrintMenuWindowString();
                    Route();
                }
            }
            catch { }
        }
    }
}
