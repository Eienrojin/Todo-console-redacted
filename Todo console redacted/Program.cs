List<string> taskList = new List<string>();
taskList.Add("Тестовая запись");
taskList.Add("Тестовая запись 2");
taskList.Add("Тестовая запись 3");

//Чтение записанного файла написать тут
//
//

WarningMessage WarningMessage = new WarningMessage();
CheckDeleteChoice CheckDeleteChoice = new CheckDeleteChoice();

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
        "\n4. ---" +
        "\n\"выход\" - для выхода");

        string answer = Console.ReadLine().Trim().ToLower();

        switch (answer)
        {
            default:
                WarningMessage.SendMessage();
                break;
            case "1":
                Console.Clear();
                InitTask();
                break;
            case "2":
                Console.Clear();

                if (CheckDeleteChoice.CheckLength(taskList, 2) == true)
                    DeleteTask();
                break;
            case "3":
                Console.Clear();
                EditTask();
                break;
            case "4":
                Console.Clear();
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
    Console.WriteLine("Напишите задачу...");
    string task = InitChoiceOrBack();

    while (true)
    {
        Console.WriteLine("Задайте приоритет:" +
            "\nA - Важное" +
            "\nB - Средняя важность" +
            "\nC - Маловажное");

        string priority = InitChoiceOrBack().ToUpper();
        if (priority == "А" || priority == "В" || priority == "С") //В условии русские символы
            priority = RussToEng(priority);

        if (priority == "A" || priority == "B" || priority == "C")
        {
            taskList.Add($"[{priority}] {task}");
            Console.WriteLine("Задача добавлена!");
            Console.ReadKey();
            MainMenu();
        }
        else
        {
            WarningMessage.SendMessage();
        }
    }
}

void DeleteTask()
{
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

    Console.Write($"{taskList.ElementAt(indexOfList)} --> ");

    choice = InitChoiceOrBack();

    taskList.RemoveAt(indexOfList);
    taskList.Insert(indexOfList, choice);
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

class WarningMessage
{
    protected bool Safe { get; set; }
    protected string Message { get; set; }

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
    public bool CheckLength(List<string> List, int value)
    {
        if (List.Count() < value)
        {
            Safe = false;
            SendMessage("Записей слишком мало для удаления");

            return Safe;
        }
        else
        {
            Safe = true;

            return Safe;
        }
    }

    //допилить
    public bool CheckOutOfRange(List<string> List)
    {
        if (List.Count() < 2)
        {
            Safe = false;
            SendMessage("Записей слишком мало для удаления");

            return Safe;
        }
        else
        {
            Safe = true;

            return Safe;
        }
    }
}
