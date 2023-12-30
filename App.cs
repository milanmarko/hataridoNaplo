using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private MenuWindow Settings;
        private MenuWindow SortedByDateToDos;
        private MenuWindow AllToDos;
        private CreateTodoWindow CreateTodo;
        private bool isAppRunning;
        private List<ToDo> ToDoList;
        private DateTime UserInput;
        public App()
        {
            getPass();
            isAppRunning = true;
            ToDoList = GetAllToDos();
            MainMenu = new MenuWindow(new string[] { "Mai teendőim", "Jövőbeli teendők megtekintése", "Új teendő hozzáadása", "Teendők megtekintése megadott dátum alapján", "Beállítások", "Kilépés" });
            Settings = new MenuWindow(new string[] { "Összes teendő törlése", "Jelszó védelem", "Az alkalmazásról" });
            UpdateLists();
            CreateTodo = new CreateTodoWindow();
            VisibleWindow = MainMenu;
        }

        private void getPass()
        {
            StreamReader sr = new StreamReader("Pass.txt");
            string passHash = sr.ReadLine();
            sr.Close();
            if (passHash != null)
            {
                string pass = "";
                do
                {
                    Console.Write("Jelszó: ");
                    pass = Console.ReadLine();
                    MD5 md5 = MD5.Create();
                    byte[] inputBytes = Encoding.UTF8.GetBytes(pass);
                    byte[] hash = md5.ComputeHash(inputBytes);
                    string hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
                    if (hashString == passHash)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Hibás jelszó");
                    }

                } while (pass != passHash);

            }
        }
        private void UpdateLists()
        {
            List<ToDo> TodaysToDosList = ToDoList.Where(x => x.Deadline.Date == DateTime.Today).ToList();
            List<ToDo> SortedByDateToDosList = ToDoList.OrderBy(x => x.Deadline.Ticks).ToList();
            TodaysToDos = new MenuWindow(TodaysToDosList.Select(x => x.Title).ToArray(), TodaysToDosList.ToArray());

            var toDosOnTheUserDate = SortedByDateToDosList.Where(x => x.Deadline.Date == UserInput.Date);
            SortedByDateToDos = new MenuWindow(toDosOnTheUserDate.Select(x => x.Title).ToArray(), toDosOnTheUserDate.ToArray());

            var allToDosFromNow = ToDoList.Where(x => x.Deadline > DateTime.Now);
            AllToDos = new MenuWindow(allToDosFromNow.Select(x => $"{x.Title} - ({x.Deadline})").ToArray(), allToDosFromNow.ToArray());
        }

        private List<ToDo> GetAllToDos()
        {
            List<ToDo> futureReturn = new List<ToDo>();
            StreamReader sr = new StreamReader("ToDos.txt", Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                string[] splittedLine = sr.ReadLine().Split(';');
                for (int i = 1; i < splittedLine.Length; i++)
                {
                    string[] toDoDetails = splittedLine[i].Split('\\');
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
                    sw.WriteLine($"{date.Year}-{date.Month}-{date.Day};{string.Join(';', toDosOnDate.Select(x => x.Title + '\\' + (x.Deadline.Hour < 10 ? '0' : string.Empty) + x.Deadline.Hour + ':' + (x.Deadline.Minute < 10 ? '0' : string.Empty) + x.Deadline.Minute + '\\' + x.Description).ToList())}");
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
                return pressedButton switch
                {
                    ConsoleKey.DownArrow => 1,
                    ConsoleKey.UpArrow => -1,
                    ConsoleKey.Enter => 0,
                    ConsoleKey.LeftArrow => 2,
                    _ => 3,
                };
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
                        VisibleWindow = AllToDos;
                        break;
                    case 2:
                        ToDoList.Add(CreateTodo.CreateTodo());
                        Save();
                        UpdateLists();
                        break;
                    case 3:
                        Console.Clear();
                        string userInputStr;
                        do
                        {
                            Console.Write("Adjon meg egy dátumot (éééé-hh-nn): ");
                            userInputStr = Console.ReadLine();
                        } while (!DateTime.TryParseExact(userInputStr, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out UserInput));
                        UpdateLists();
                        VisibleWindow = SortedByDateToDos;
                        break;
                    case 4:
                        VisibleWindow = Settings;
                        break;
                    case 5:
                        Save();
                        isAppRunning = false;
                        break;
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
            else if (VisibleWindow == SortedByDateToDos)
            {
                int originalIndex = VisibleWindow.SelectedIndex;
                PreviousWindow = MainMenu;
                while (VisibleWindow == SortedByDateToDos)
                {
                    SortedByDateToDos.ShowToDoDetails(originalIndex);
                    Route();
                }
            }
            else if(VisibleWindow == AllToDos)
            {
                int originalIndex = VisibleWindow.SelectedIndex;
                PreviousWindow = MainMenu;
                while (VisibleWindow == AllToDos)
                {
                    AllToDos.ShowToDoDetails(originalIndex);
                    Route();
                }
            }
            else if (VisibleWindow == Settings)
            {
                PreviousWindow = MainMenu;
                switch (WindowGlobalIndex)
                {
                    case 0:
                        Console.Clear();
                        Console.Write("Biztosan törölni szeretnéd az összes teendőt? (i/n)");
                        if (Console.ReadKey().Key == ConsoleKey.I)
                        {
                            ToDoList.Clear();
                            Save();
                            UpdateLists();
                            Console.Write("\nSikeresen törölve");
                            Console.ReadKey();
                        }
                        break;
                    case 1:
                        string pass = "";
                        do
                        {
                            Console.Write("Jelszó: ");
                            pass = Console.ReadLine();
                            if (pass == "")
                            {
                                Console.Write("Jelszó nem lehet üres");
                                Console.ReadKey();
                            }
                        } while (pass == "");
                        MD5 md5 = MD5.Create();
                        byte[] inputBytes = Encoding.UTF8.GetBytes(pass);
                        byte[] hash = md5.ComputeHash(inputBytes);
                        string hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
                        StreamWriter sw = new StreamWriter("Pass.txt");
                        sw.Write(hashString);
                        sw.Close();
                        Console.Write("Sikeresen beállítva");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Készítette: Markó Milán és Molnár Bálint");
                        Console.WriteLine("Verzió: 0.8");
                        Console.WriteLine("Készült: 2023.11.31. - 2024.01.01");
                        Console.WriteLine("Nyelv: C#");
                        Console.WriteLine("Környezet: Visual Studio 2022");
                        Console.WriteLine("Köszönet: A programozás tanárunknak, aki segített a program elkészítéséhez szükséges felkészítésben");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public void Route()
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
            else if ((VisibleWindow == MainMenu || (VisibleWindow == AllToDos && VisibleWindow.isThereAnyTodo()) || (VisibleWindow == TodaysToDos && VisibleWindow.isThereAnyTodo()) || (VisibleWindow == SortedByDateToDos && VisibleWindow.isThereAnyTodo()) || VisibleWindow == Settings) && !VisibleWindow.ShowingDetails)
            {
                OpenWindow(VisibleWindow.SelectedIndex);
            }
            else if ((VisibleWindow == TodaysToDos || VisibleWindow==SortedByDateToDos || VisibleWindow == AllToDos) && VisibleWindow.ShowingDetails && VisibleWindow.SelectedIndex == 3)
            {
                Console.Clear();
                if (ToDoList.Remove(VisibleWindow.DetailedToDo))
                {
                    UpdateLists();
                    Console.WriteLine("Sikeresen törölve!");
                }
                else
                {
                    Console.WriteLine("Nem sikerült törölni, kérem probálja újra!");
                }
                VisibleWindow = PreviousWindow;
            }
        }
        public void RunApp()
        {
            while (isAppRunning)
            {
                VisibleWindow.PrintMenuWindowString();
                Route();
            }
        }
    }
}
