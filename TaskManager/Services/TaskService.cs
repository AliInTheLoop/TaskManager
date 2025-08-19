using TaskManager.Models;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.Services
{
    public class TaskService
    {
        private List<TaskItem> tasks = new();
        private int nextId = 1;

        public void Load(List<TaskItem> loadedTasks)
        {
            tasks = loadedTasks;
            if (tasks.Count > 0)
                nextId = tasks.Max(t => t.Id) + 1;
        }

        public List<TaskItem> GetAll() => tasks;

        public void AddTask(TaskItem task)
        {
            task.Id = nextId++;
            tasks.Add(task);
        }

        // TODO: Add methods like UpdateTask, DeleteTask, FilterTasks, etc.


        public void ViewAllTasks()
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"\nID: {task.Id}");
                Console.WriteLine($"Title: {task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Due Date: {task.DueDate:d}");
                Console.WriteLine($"Priority: {task.Priority}");
                Console.WriteLine($"Status: {task.Status}");
            }
        }

        public void UpdateTask(List<TaskItem> taskFromOutside, int id)
        {
            //Lambda separates the parameter from the expression, t => expression
            var task = taskFromOutside.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                Console.WriteLine($"Task with ID {id} not found");
            }

            //Titel
            Console.WriteLine("Enter a new Titel: ");
            var title = Console.ReadLine();
            Console.WriteLine($"Updated Title: {title}");

            //Description
            while (true)
            {
                Console.WriteLine("Do you want to change the Description: (yes/no)");
                var input = Console.ReadLine();

                if (input == "yes")
                {
                    Console.WriteLine("Enter new description: ");
                    var newDescription = Console.ReadLine();
                    task.Description = newDescription;
                    Console.WriteLine($"Updated Description: {task.Description}");
                    break;
                } 
                if (input == "no")
                {
                    Console.WriteLine("Description not changed! ");
                    break;
                }
                Console.WriteLine("Please type yes or no: ");
            }

            //Due DAte
            while (true)
            {
                Console.WriteLine("Enter a Due Date yyyy/MM/dd: ");
                var inputDate = Console.ReadLine();

                DateTime dueDate;
                if (DateTime.TryParse(inputDate, out dueDate))
                {
                    task.DueDate = dueDate;
                    Console.WriteLine($"Updated Due Date: {dueDate:d}");
                    break;
                }
                Console.WriteLine("Invalid input. Please type yyyy/MM/dd");
            }

            //Priority
            while (true)
            {
                Console.WriteLine("Enter a Priority: low, medium, high");
                var inputPriority = Console.ReadLine();

                if (Enum.TryParse(inputPriority, true, out TaskPriority priority))
                {
                    task.Priority = priority;

                    switch (priority)
                    {
                        case TaskPriority.Low:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case TaskPriority.Medium:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case TaskPriority.High:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Updated Priority: {task.Priority}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please type low, medium or high");
                }
            }

            //Status
            while (true)
            {
                Console.WriteLine("Enter a Status: (Pending/ Completed): ");
                var inputStatus = Console.ReadLine();

                Enum.TryParse<TaskStatus>(inputStatus, true, out var status);
                {
                    task.Status = status;

                    switch (status)
                    {
                        case TaskStatus.Pending:
                            Console.WriteLine("Updated Status: Pending");
                            break;
                        case TaskStatus.Completed:
                            Console.WriteLine("Updated Status: Completed");
                            break;
                    }
                    Console.WriteLine($"Updated Status: {status}");
                    break;
                }
            }
        }

        //Delete task
        public void DeleteTask(int id)
        {
            while (true)
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                
                Console.WriteLine($"Enter a ID to delete:  (type no to quit)");
                var input = Console.ReadLine();
                if (input == "no")
                {
                    Console.WriteLine("Goodbye.");
                    Environment.Exit(0);
                }

                if (int.TryParse(input, out var idToDelete))
                {
                    var taskToDelete = tasks.FirstOrDefault(t => t.Id == idToDelete);

                    if (taskToDelete != null)
                    {
                        tasks.Remove(taskToDelete);
                        Console.WriteLine($"Your task with the {idToDelete} was deleted"); 
                        break;
                    }
                    Console.WriteLine($"No task with this ID was found"); 
                    
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number");
                }
            }
        }

        public TaskItem? MarkTaskAsCompleted(int id)
        {
            var singleTask = tasks.FirstOrDefault(t => t.Id == id);

            if (singleTask == null)
            {
                Console.WriteLine($"No task with {id} was found");
            }
            
            var completed = singleTask.Status == TaskStatus.Completed;
            Console.WriteLine($"Task, {completed} marked as completed");
            return completed ? singleTask : null;
        }
    }
}