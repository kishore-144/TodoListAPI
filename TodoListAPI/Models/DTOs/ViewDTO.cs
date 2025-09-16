namespace TodoListAPI.Models.DTOs
{
    public class ViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public bool IsDone { get; set; }
    }
}
