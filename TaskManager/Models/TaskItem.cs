namespace TaskManager.Models
{
    public enum TaskPriority { Low, Medium, High }
    public enum TaskStatus { Pending, Completed }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
    }
}