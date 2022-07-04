using Microsoft.AspNetCore.Mvc;
using TaskTracker.Requests;
using TaskTracker.Requests.ProjectRequest;
using TaskTracker.Requests.UserRequest;
using TaskTracker.Responses.ProjectResponses;
using TaskTracker.Validation.UserValidation;
using TaskTrackerModels;
using TaskTrackerServices;

namespace TaskTracker.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        public ProjectController(IUserService userService, IProjectService projectService)
        {
            _userService = userService;
            _projectService = projectService;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("createProject")]
        public ActionResult<string> CreateProject([System.Web.Http.FromUri] NewProjectRequest request)
        {
            bool user = _userService.CheckUser(request.UserLogin, request.UserPassword);

            if (!user) return BadRequest("Пользователь не существует или неправильный пароль");

            if(request.Name == null || request.Name == "") return BadRequest("Название не может быть пустым");
            if(request.Description == null || request.Description == "") return BadRequest("Описание не может быть пустым");

            var project = _projectService.CreateProject(request.UserLogin, request.Name, request.Description, request.IsPersonal);

            if (project == null) return BadRequest("Проект не был создан");
            return StatusCode(201, new JsonResult(project.Id));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("getProjects")]
        public ActionResult<List<ProjectMainPageResponse>> GetProjects(string login, string password)
        {
          
            bool user = _userService.CheckUser(login, password);


            if (!user) return BadRequest("Пользователь не существует или неправильный пароль");
            var data = _projectService.GetMyProjects(login);

            var projects = new List<ProjectMainPageResponse>();

            foreach(var unit in data)
            {
                var project = new ProjectMainPageResponse();
                project.Id = unit.Id;
                project.Name = unit.Name;
                project.IsPersonal = unit.IsPersonal;
                project.usersCount = unit.UsersCount;
                project.IsUserIn = unit.IsUserIn;
                project.Author = unit.Author;
                projects.Add(project);
            }

            return StatusCode(200, new JsonResult(projects));

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("watchProject")]
        public ActionResult<ProjectShortDto> WatchProject(string login, string password, Guid id)
        {
           
            bool user = _userService.CheckUser(login, password);

            if (!user) return BadRequest("Пользователь не существует или неправильный пароль");

            var data = _projectService.WatchProject(id, login);
            if(data == null) return BadRequest("Проект не может быть найден или больше не существует");
            return StatusCode(200, new JsonResult(data));

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("goToProject")]
        public ActionResult<ProjectMainDto> GoToProject(string login, string password, Guid id)
        {

            bool user = _userService.CheckUser(login, password);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");

            bool isLet = _userService.isAllowedToWatchProject(login, id);
            if (!isLet) return BadRequest("Not allowed to watch");
            var data = _projectService.GoToProject(id, login);
            if (data == null) return BadRequest("The project can`t be found or doesn`t exist anymore");
            return StatusCode(200, new JsonResult(data));

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("joinProject")]
        public ActionResult<string> JoinProject([System.Web.Http.FromUri] JoinProjectRequest request)
        {
            bool user = _userService.CheckUser(request.Login, request.Password);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");



            var project = _projectService.JoinProject(request.Id, request.Login);

            if (project == null) return BadRequest("Error: this user did not join the project");
            return StatusCode(201, new JsonResult("User has joined the project!"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("changeProject")]
        public ActionResult<string> ChangeProject([System.Web.Http.FromUri] ChangeProjectRequest request)
        {
            bool user = _userService.CheckUser(request.Login, request.Password);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");

            

            var project = _projectService.UpdateProject(request.Id, request.Login, request.newName, request.newDesc);

            if (project == null) return BadRequest("Error: project was not changed");
            return StatusCode(201, new JsonResult("Project has been changed!"));
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("deleteProject")]
        public ActionResult<string> DeleteProject([System.Web.Http.FromUri] DeleteProjectRequest request)
        {
            bool user = _userService.CheckUser(request.Login, request.Password);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");



            bool project = _projectService.DeleteProject(request.Id, request.Login);

            if (!project) return BadRequest("Error: project was not deleted");
            return StatusCode(201, new JsonResult("Project has been deleted!"));

        }

    }
}
