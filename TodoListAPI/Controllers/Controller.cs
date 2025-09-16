using Microsoft.AspNetCore.Mvc;
using TodoListAPI.Models;
using TodoListAPI.Repository;
using TodoListAPI.Models.DTOs;

namespace TodoListAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController:ControllerBase
    {

        private readonly ITaskRepositories _iRepo;
        public TaskController(ITaskRepositories iRepo)
        {
            _iRepo = iRepo;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ViewDTO>>> GetAll()
        {
            try
            {
                var returnResult = await _iRepo.GetAllTasks();
                return returnResult;
            }
            catch (Exception ex)
            {
                var returnResult = StatusCode(500, new ApiResponse
                {
                    status = "Failure",
                    message = $"Unexpected error in GetAll: {ex.Message}"
                });
                return returnResult;
            }
        }
        [HttpPost("")]
        public async Task<ActionResult<ApiResponse>> AddTask([FromBody] PostRequestBody request)
        {
            try
            {
                var returnResult = await _iRepo.AddTask(request);
                return returnResult;
            }
            catch (Exception ex)
            {
                var returnResult = StatusCode(500, new ApiResponse
                {
                    status = "Failure",
                    message = $"Unexpected error: {ex.Message}"
                });
                return returnResult;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateTask([FromBody] PostRequestBody request, int id)
        {
            try
            {
                var updater = await _iRepo.UpdateTask(request, id);
                return updater;
            }
            catch (Exception ex)
            {
                var notUpdated = StatusCode(500, new ApiResponse
                {
                    status = "Failure",
                    message = $"Unexpected Error: {ex}"
                });
                return notUpdated;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteTask(int id)
        {
            try
            {
                var deleted = await _iRepo.DeleteTask(id);
                return deleted;
            }
            catch (Exception ex)
            {
                var notDeleted = StatusCode(500, new ApiResponse
                {
                    status = "Failure",
                    message = $"Unexpected Error: {ex}"
                });
                return notDeleted;
            }
        }
        [HttpPut("done/{id}")]
        public async Task<ActionResult<ApiResponse>> DoneHandler(int id)
        {
            try
            {
                var updater = await _iRepo.DoneHandler(id);
                return updater;
            }
            catch (Exception ex)
            {
                var notUpdated = StatusCode(500, new ApiResponse
                {
                    status = "Failure",
                    message = $"Unexpected Error: {ex}"
                });
                return notUpdated;
            }
        }
    }
}
