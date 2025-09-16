using TodoListAPI.Models;
using TodoListAPI.Models.DTOs;
namespace TodoListAPI.Repository
{
    public interface ITaskRepositories
    {
        Task<List<ViewDTO?>> GetAllTasks();
        Task<ApiResponse> AddTask(PostRequestBody newTask);
        Task<ApiResponse> UpdateTask(PostRequestBody updatedTask, int TaskId);
        Task<ApiResponse> DeleteTask(int Id);
        Task<ApiResponse> DoneHandler(int Id);
    }
}
