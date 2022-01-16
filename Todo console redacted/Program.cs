List<string> taskList = new List<string>();

WarningMessage WarningMessage = new WarningMessage();
CheckDeleteChoice CheckList = new CheckDeleteChoice();

string fileName = "SavedTasks.txt";
string fullPath = Path.GetFullPath(fileName);

using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
{
    if (!File.Exists(fullPath))
        File.Create(fullPath);
}

ReadFile();
MainMenu();

void MainMenu()
{
    while (true)
    {
        Console.Clear();

        ShowTaskList();
        Console.WriteLine("Что вы хотите сделать?" +
        "\n1. Добавить задачу" +
        "\n2. Удалить задачу" +
        "\n3. Изменить задачу" +
        "\n4. Сохраниться" +
        "\n\"выход\" - для выхода");

        string answer = Console.ReadLine().Trim().ToLower();

        switch (answer)
        {
            default:
                WarningMessage.SendMessage();
                break;
            case "1":
                InitTask();
                break;
            case "2":
                if (CheckList.CheckLength(taskList) == true)
                    DeleteTask();
                break;
            case "3":
                EditTask();
                break;
            case "4":
                SaveFile(taskList);
                break;
            case "выход":
                Environment.Exit(0);
                break;
        }
        Console.Clear();
    }
}

void InitTask()
{
    Console.Clear();
    Console.WriteLine("Напишите задачу...");
    string task = InitChoiceOrBack();

    string taskPriority = InitPriority(task);
    taskList.Add($"[{taskPriority}] {task}");
}

string InitPriority(string task)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Задайте приоритет:" +
            "\nA - Важное" +
            "\nB - Средняя важность" +
            "\nC - Маловажное");

        string taskPriority = InitChoiceOrBack().ToUpper();
        if (taskPriority == "А" || taskPriority == "В" || taskPriority == "С") //В условии русские символы
            taskPriority = RussToEng(taskPriority);

        if (taskPriority == "A" || taskPriority == "B" || taskPriority == "C")
        {
            Console.WriteLine("Приоритет добавлен!");
            Console.ReadKey();
            return taskPriority;
        }
        else
        {
            WarningMessage.SendMessage();
        }
    }
}

void DeleteTask()
{
    Console.Clear();
    ShowTaskList();

    Console.WriteLine("Введите номер записи, которую вы хотите удалить...");

    string choice = InitChoiceOrBack();
    int intChoice = 0;

    try
    {
        intChoice = Convert.ToInt32(choice);
    }
    catch (FormatException)
    {
        WarningMessage.SendMessage();
        DeleteTask();
    }

    taskList.RemoveAt(intChoice - 1);
}

void EditTask()
{
    Console.Clear();
    ShowTaskList();

    Console.WriteLine("Введите номер записи, которую вы хотите изменить...");

    string choice = InitChoiceOrBack();
    int indexOfList = 0;

    try
    {
        indexOfList = Convert.ToInt32(choice);
    }
    catch (FormatException)
    {
        WarningMessage.SendMessage();
        EditTask();
    }

    indexOfList -= 1;
    bool outOfRange = CheckList.CheckOutOfRange(taskList, indexOfList);

    if (!outOfRange)
    {
        EditTask();
    }
    else
    {
        Console.Write($"[{taskList.ElementAt(indexOfList)}] ");

        choice = InitChoiceOrBack();

        string choicePriority = InitPriority(choice);

        taskList.RemoveAt(indexOfList);
        taskList.Insert(indexOfList, $"[{choicePriority}] {choice}");
    }
}

//Второстепенные методы 
void ShowTaskList()
{
    int counter = 1;

    Console.WriteLine("----------\n");

    foreach (string task in taskList)
    {
        Console.WriteLine($"{counter}. {task}");
        counter++;
    }

    Console.WriteLine("\n----------\n");
}

string InitChoiceOrBack()
{
    Console.WriteLine("Если хотите вернуться назад, введите \"назад\"");

    string choice = Console.ReadLine().Trim();

    if (choice.ToLower() == "назад")
        MainMenu();

    return choice;
}

//Конвертация русских символов в английские
string RussToEng(string choice)
{
    if (choice == "А")
        choice = "A";

    if (choice == "В")
        choice = "B";

    if (choice == "С")
        choice = "C";

    return choice;
}

//Сохранение и чтение
void ReadFile()
{
    string[] lines = File.ReadAllLines(fullPath);

    taskList = lines.ToList();
}

void SaveFile(List<string> taskList)
{
    File.WriteAllLines(fullPath, taskList);
}

class WarningMessage
{
    protected bool Safe { get; set; }

    //Для вывода ошибки внутри чей-то логики с выбором надписи
    protected void SendMessage(string message)
    {
        Console.WriteLine(message +
            "\nДля продолжения нажмите любую клавишу...");
        Console.ReadKey();
    }

    //Для простого вывода ошибки
    public void SendMessage()
    {
        Console.WriteLine("Что-то пошло не так, проверьте правильность выбора." +
            "\nДля продолжения нажмите любую клавишу...");
        Console.ReadKey();
    }
}

class CheckDeleteChoice : WarningMessage
{
    public bool CheckLength(List<string> List)
    {
        if (List.Count() < 1)
        {
            Safe = false;
            SendMessage("Нет записей для удаления");

            return Safe;
        }
        else
        {
            Safe = true;

            return Safe;
        }
    }
    public bool CheckOutOfRange(List<string> List, int value)
    {
        if (value <= -1|| List.Count() < value)
        {
            Safe = false;
            SendMessage("Такой записи нет");

            return Safe;
        }
        else
        {
            Safe = true;

            return Safe;
        }
    }
}
