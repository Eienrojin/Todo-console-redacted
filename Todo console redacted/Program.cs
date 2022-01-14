List<string> taskList = new List<string>();
taskList.Add("Тестовая запись");

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
        "\n3. ---" +
        "\n4. ---" +
        "\nexit - выход");

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
                Console.Clear();

                if (CheckDeleteChoice.CheckChoice(taskList))
                    DeleteTask();
                break;
            case "3":
                break;
            case "4":
                break;
            case "exit":
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

void DeleteTask()
{
    Console.Clear();
    ShowTaskList();

    Console.WriteLine("Введите номер записи, которую вы хотите удалить... ");

    int intChoice = 0;
    string choice = InitChoiceOrBack();

    try
    {
        intChoice = Convert.ToInt32(choice);
        taskList.RemoveAt(intChoice - 1);
    }
    catch (ArgumentOutOfRangeException)
    {
        WarningMessage.SendMessage();
        DeleteTask();
    }
}

string InitChoiceOrBack()
{
    Console.WriteLine("Если хотите вернуться назад, введите return");

    string choice = Console.ReadLine().Trim();

    if (choice.ToLower() == "return")
        MainMenu();

    return choice;
}

class WarningMessage
{
    protected bool Safe { get; set; }
    protected string Message { get; set; }

    protected void SendMessage(string message)
    {
        Message = message;

        Console.WriteLine(Message +
            "\nДля продолжения нажмите любую клавишу...");
        Console.ReadKey();
    }

    public void SendMessage()
    {
        Console.WriteLine("Что-то пошло не так, проверьте правильность выбора." +
            "\nДля продолжения нажмите любую клавишу...");
        Console.ReadKey();
    }
}

//Попробовать использовать виртуальный метод
class CheckDeleteChoice : WarningMessage
{
    public bool CheckChoice(List<string> List)
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