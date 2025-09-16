using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Models.DTOs;

namespace TodoListAPI.Repository
{
    public class TaskRepositories:ITaskRepositories
    {
        private readonly AppDbContext _context;

        public TaskRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ViewDTO?>> GetAllTasks()
        {
            try
            {
                var tasks = await _context.ToDoTasks
                    .Select(e => new ViewDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                        IsDeleted = e.IsDeleted,
                        IsDone=e.IsDone
                    })
                    .Where(e=>e.IsDeleted==false)
                    .ToListAsync();
                return tasks;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ApiResponse> AddTask(PostRequestBody newTask)
        {
            try
            {
                if (newTask == null)
                {
                    return new ApiResponse
                    {
                        status = "Failure",
                        message = "Event details not sufficient"
                    };
                }
                //var exists = await _context.ToDoTasks
                //    .AnyAsync(e => e.Name.ToLower() == newTask.Name.ToLower());
                //if (exists)
                //{
                //    return new ApiResponse
                //    {
                //        status = "Failure",
                //        message = $"Task already exists"
                //    };
                //}
                var ToAdd = new ToDoTasks
                {
                    Name = newTask.Name,
                    Createdat = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")),
                    IsDeleted=false,
                    IsDone = false
                };
                _context.ToDoTasks.Add(ToAdd);
                await _context.SaveChangesAsync();
                return new ApiResponse
                {
                    status = "Success",
                    message = "Created Successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    status = "Failure",
                    message = $"Error: {ex.Message}"
                };
            }
        }
        public async Task<ApiResponse> UpdateTask(PostRequestBody updatedTask, int TaskId)
        {
            try
            {
                var toBeUpdated = await _context.ToDoTasks.FindAsync(TaskId);
                if (toBeUpdated == null)
                {
                    return new ApiResponse
                    {
                        status = "Failure",
                        message = $"No Task exists with id: {TaskId}"
                    };
                }
                toBeUpdated.Name = updatedTask.Name;
                await _context.SaveChangesAsync();
                return new ApiResponse
                {
                    status = "Success",
                    message = "Updated Successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    status = "Failure",
                    message = $"Error: {ex.Message}"
                };
            }
        }
        public async Task<ApiResponse> DeleteTask(int Id)
        {
            try
            {
                var toBeDeleted = await _context.ToDoTasks.FindAsync(Id);
                if (toBeDeleted == null)
                {
                    return new ApiResponse
                    {
                        status = "Failure",
                        message = $"No Task exists with id: {Id}"
                    };
                }
                toBeDeleted.IsDeleted = true;
                toBeDeleted.Updatedat = DateTimeOffset.UtcNow;
                _context.ToDoTasks.Update(toBeDeleted);
                await _context.SaveChangesAsync();
                return new ApiResponse
                {
                    status = "Success",
                    message = "Deleted Successfully (soft delete)"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    status = "Failure",
                    message = $"Error: {ex.Message}"
                };
            }
        }
        public async Task<ApiResponse> DoneHandler(int Id)
        {
            try
            {
                var toBeUpdated = await _context.ToDoTasks.FindAsync(Id);
                if (toBeUpdated == null)
                {
                    return new ApiResponse
                    {
                        status = "Failure",
                        message = $"No Task exists with id: {Id}"
                    };
                }
                toBeUpdated.IsDone = !toBeUpdated.IsDone;
                await _context.SaveChangesAsync();
                if (toBeUpdated.IsDone)
                {
                    return new ApiResponse
                    {
                        status = "Success",
                        message = "Task Completed!!!"
                    };
                }
                else
                {
                    return new ApiResponse
                    {
                        status = "Success",
                        message = "Task yet to be completed!!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    status = "Failure",
                    message = $"Error: {ex.Message}"
                };
            }
        }
    }
}
