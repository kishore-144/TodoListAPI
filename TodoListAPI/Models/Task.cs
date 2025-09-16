namespace TodoListAPI.Models
{
    public class ToDoTasks
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTimeOffset? Createdat { get; set; }
        public DateTimeOffset? Updatedat { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDone { get; set; }
    }
}
