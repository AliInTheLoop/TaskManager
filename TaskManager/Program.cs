using TaskManager.Models;
using TaskManager.Services;
namespace TaskManager;

class Program
{
    static TaskService service = new();

    static void Main()
    {
        Console.WriteLine("=== Task Manager ===");

        var loadedTasks = TaskStorage.LoadTasks();
        service.Load(loadedTasks);

        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ListTasks();
                    break;
                case "3":
                    UpdateTask();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5" :
                    MarkTaskAsCompleted();
                    break;
                case "6":
                    SearchTaskTitle();
                    break;
                case "8":
                    Exit();
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n1. Add new task");
        Console.WriteLine("2. View all tasks");
        Console.WriteLine("3. Update task");
        Console.WriteLine("4. Delete task");
        Console.WriteLine("5. Mark Task as completed");
        Console.WriteLine("6. Search Task by Title");
        // More options to be implemented
        Console.WriteLine("8. Save & Exit");
        Console.Write("Choose an option: ");
    }

    static void AddTask()
    {
        Console.Write("Title: ");
        var title = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Description (optional): ");
        var desc = Console.ReadLine();

        Console.Write("Due Date (yyyy-mm-dd): ");
        DateTime dueDate = DateTime.Parse(Console.ReadLine() ?? "");

        Console.Write("Priority (Low/Medium/High): ");
        TaskPriority priority = Enum.Parse<TaskPriority>(Console.ReadLine() ?? "", true);

        var task = new TaskItem
        {
            Title = title,
            Description = desc,
            DueDate = dueDate,
            Priority = priority
        };

        service.AddTask(task);
        Console.WriteLine("Task added.");
    }

    static void ListTasks()
    {
        service.ViewAllTasks();
    }

    static void Exit()
    {
        TaskStorage.SaveTasks(service.GetAll());
        Console.WriteLine("Tasks saved. Goodbye!");
    }
    
    static void UpdateTask()
    {
        // 1. User nach der ID fragen
        Console.Write("Enter the ID of the task you want to update: ");
        var input = Console.ReadLine();

        if (!int.TryParse(input, out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }
        
        var allTasks = service.GetAll();
        
        service.UpdateTask(allTasks, id);
    }

    static void DeleteTask()
    {
        service.DeleteTask();
    }

    static void MarkTaskAsCompleted()
    {
        service.Load(new List<TaskItem> {
            new TaskItem { Id = 2, Title = "Test1" },
            new TaskItem { Id = 5, Title = "Test2" },
            new TaskItem { Id = 10, Title = "Test3" }
        });

        var completedTask = new List<int> { 2, 5, 10 };
        service.MarkTaskAsCompleted(2);

        foreach (var id in completedTask)
        {
            service.MarkTaskAsCompleted(id);
        }
    }
    
    static void SearchTaskTitle()
    {
        service.SearchTaskByTitle();
    }
}