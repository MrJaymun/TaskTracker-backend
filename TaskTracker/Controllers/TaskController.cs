    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Requests.TaskRequest;
using TaskTrackerModels.Tasks;
using TaskTrackerServices;

namespace TaskTracker.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        public TaskController(IUserService userService, ITaskService taskService)
        {
            _userService = userService;
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("createTask")]
        public ActionResult<string> CreateTask([System.Web.Http.FromUri] NewTaskRequest request)
        {
            bool user = _userService.CheckUser(request.UserLogin, request.UserPassword);

            if (!user) return BadRequest("Пользователь не существует или неправильный пароль");

            if (request.Name == null || request.Name == "") return BadRequest("Имя не может быть пустым");
            if (request.Description == null || request.Description == "") return BadRequest("Описание не может быть пустым");
            if (request.PerformerLogin == null || request.PerformerLogin == "") return BadRequest("У задачи должен быть исполнитель");


            var task = _taskService.CreateTask(request.Name, request.Description, request.Project, request.PerformerLogin);

            if (task == null) return BadRequest("Задача не была создана");
            return StatusCode(201, new JsonResult(task.Id));
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("getTasks")]
        public ActionResult<List<TaskWatchDto>> GetTasks(string login, string password, Guid id)
        {

            bool user = _userService.CheckUser(login, password);


            if (!user) return BadRequest("There is no such a user in database or password is incorrect");

            bool isLet = _userService.isAllowedToWatchProject(login, id);
            if (!isLet) return BadRequest("Not allowed to watch");

            var data = _taskService.GetTasks(id);
            if (data == null) return BadRequest("Project not found or there is no tasks");
           
            return StatusCode(200, new JsonResult(data));

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("getTask")]
        public ActionResult<List<TaskWatchDto>> GetTask(string login, string password, Guid projectId, Guid taskId)
        {

            bool user = _userService.CheckUser(login, password);


            if (!user) return BadRequest("There is no such a user in database or password is incorrect");

            bool isLet = _userService.isAllowedToWatchProject(login, projectId);
            if (!isLet) return BadRequest("Not allowed to watch");

            var data = _taskService.GetTask(projectId, taskId);
            if (data == null) return BadRequest("Task not found");

            return StatusCode(200, new JsonResult(data));

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("updateTask")]
        public ActionResult<string> UpdateTask([System.Web.Http.FromUri] UpdateTaskRequest request)
        {
            bool user = _userService.CheckUser(request.UserLogin, request.UserPassword);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");

            if (request.Name == null || request.Name == "") return BadRequest("Name can not be empty");
            if (request.Description == null || request.Description == "") return BadRequest("Description can not be empty");

            bool isLet = _userService.isAllowedToWatchProject(request.UserLogin, request.ProjectId);
            if (!isLet) return BadRequest("Not allowed to update");

            bool isTask = _userService.isTaskOnProject(request.TaskId, request.ProjectId);
            if (!isTask) return BadRequest("Task is not at project");


            bool task = _taskService.UpdateTask(request.TaskId, request.Name, request.Description);

            if (!task) return BadRequest("Task was not updated");
            return StatusCode(200, new JsonResult("Task was updated"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("updateTaskStatus")]
        public ActionResult<string> UpdateTaskStatus([System.Web.Http.FromUri] UpdateTaskStatusRequest request)
        {
            bool user = _userService.CheckUser(request.UserLogin, request.UserPassword);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");

            if (request.Status == null || request.Status == "") return BadRequest("Status can not be empty");

            bool isLet = _userService.isAllowedToWatchProject(request.UserLogin, request.ProjectId);
            if (!isLet) return BadRequest("Not allowed to update");

            bool isTask = _userService.isTaskOnProject(request.TaskId, request.ProjectId);
            if (!isTask) return BadRequest("Task is not at project");


            bool task = _taskService.UpdateTaskStatus(request.TaskId, request.Status);

            if (!task) return BadRequest("Task was not updated");
            return StatusCode(200, new JsonResult("Task was updated"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("deleteTask")]
        public ActionResult<string> DeleteTask([System.Web.Http.FromUri] DeleteTaskRequest request)
        {
            bool user = _userService.CheckUser(request.UserLogin, request.UserPassword);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");


            bool isLet = _userService.isAllowedToWatchProject(request.UserLogin, request.ProjectId);
            if (!isLet) return BadRequest("Not allowed to delete");

            bool isTask = _userService.isTaskOnProject(request.TaskId, request.ProjectId);
            if (!isTask) return BadRequest("Task is not at project");


            bool task = _taskService.DeleteTask(request.TaskId);

            if (!task) return BadRequest("Task was not deleted");
            return StatusCode(200, new JsonResult("Task was deleted"));
        }



    }
}
