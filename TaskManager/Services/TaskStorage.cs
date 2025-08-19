using System.Text.Json;
using TaskManager.Models;

namespace TaskManager.Services
{
    public static class TaskStorage
    {
        private static readonly string FilePath = "tasks.json";

        public static List<TaskItem> LoadTasks()
        {
            if (!File.Exists(FilePath)) return new List<TaskItem>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        public static void SaveTasks(List<TaskItem> tasks)
        {
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}