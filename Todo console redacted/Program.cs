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
    Console.WriteLine("Напишите задачу... Чтобы отменить, напишите return");

    string task = Console.ReadLine().Trim();

    if (task == "return")
        MainMenu();

    taskList.Add(task);

    Console.WriteLine("Задача добавлена!");
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
    ShowTaskList();

    Console.WriteLine("Введите номер записи, которую вы хотите удалить... ");
    PrintReturnBack();

    string choice = Console.ReadLine().Trim().ToLower();

    if (choice == "return")
        MainMenu();

    taskList.RemoveAt(Convert.ToInt32(choice));

}

void PrintReturnBack()
{
    Console.WriteLine("\nЕсли хотите вернуться назад, введите return");
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