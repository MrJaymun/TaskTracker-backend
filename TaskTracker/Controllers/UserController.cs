using Microsoft.AspNetCore.Mvc;
using TaskTracker.Requests;
using TaskTracker.Requests.UserRequest;
using TaskTracker.Validation.UserValidation;
using TaskTrackerModels;
using TaskTrackerServices;



namespace TaskTracker.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("createUser")]
        public ActionResult<string> CreateUser([System.Web.Http.FromUri] NewUserRequest request)
        {

            if(!UserValidator.ValidateLogin(request.NewUserLogin)) return BadRequest("Incorrect symbols in login.");
            if(!UserValidator.ValidatePassword(request.NewUserPassword)) return BadRequest("Incorrect symbols in password.");

            var user = _userService.CreateUser(request.NewUserLogin, request.NewUserPassword);

            if(user == null) return BadRequest("Login is used by another user");

            return StatusCode(201, new JsonResult("User with login " + user.Login + " was successfully created"));
          
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("changeUserPassword")]
        public ActionResult<string> ChangeUserPassword([System.Web.Http.FromUri] NewPasswordRequest request)
        {

            
            if (!UserValidator.ValidatePassword(request.userNewPassword)) return BadRequest("Incorrect symbols in new password.");

            var user = _userService.UpdatePassword(request.userLogin, request.userPassword, request.userNewPassword);

            if (user == null) return BadRequest("User does not exist or password is incorrect");

            return StatusCode(201, new JsonResult("Password of user with login " + user.Login + " was successfully changed"));

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("doesUserExist")]
        public ActionResult<string> DoesUserExist(string login, string password)
        {
           
               bool user = _userService.CheckUser(login, password);

                if (user) return StatusCode(200, new JsonResult("true"));
            return BadRequest("There is no such a user in database or password is incorrect");

            
        }
    }
}
