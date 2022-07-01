using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Requests.CommentRequest;
using TaskTrackerServices;

namespace TaskTracker.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        public CommentController(IUserService userService, ICommentService commentService)
        {
            _userService = userService;
            _commentService = commentService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("createComment")]
        public ActionResult<string> CreateComment([System.Web.Http.FromUri] NewCommentRequest request)
        {
            bool user = _userService.CheckUser(request.UserLogin, request.UserPassword);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");

            bool isLet = _userService.isAllowedToWatchProject(request.UserLogin, request.ProjectId);
            if (!isLet) return BadRequest("Not allowed to watch");
    
            bool isTask = _userService.isTaskOnProject(request.TaskId, request.ProjectId);
            if (!isTask) return BadRequest("Task is not at project");

            if (request.Text == null || request.Text == "") return BadRequest("Text of comment can not be empty");

            var comment = _commentService.CreateComment(request.TaskId, request.UserLogin, request.Text);

            if (comment == null) return BadRequest("Comment was not created");
            return StatusCode(201, new JsonResult("Comment was created!"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("updateComment")]
        public ActionResult<string> UpdateComment([System.Web.Http.FromUri] UpdateCommentRequest request)
        {
            bool user = _userService.CheckUser(request.UserLogin, request.UserPassword);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");

            bool isLet = _userService.isAllowedToWatchProject(request.UserLogin, request.ProjectId);
            if (!isLet) return BadRequest("Not allowed to watch");

            bool isTask = _userService.isTaskOnProject(request.TaskId, request.ProjectId);
            if (!isTask) return BadRequest("Task is not at project");

            bool isComment = _userService.isCommentOnTask(request.CommentId, request.TaskId);
            if (!isComment) return BadRequest("Comment is not at task");

            if (request.Text == null || request.Text == "") return BadRequest("Text of comment can not be empty");

            var comment = _commentService.UpdateComment(request.CommentId, request.Text);

            if (comment == null) return BadRequest("Comment was not updated");
            return StatusCode(201, new JsonResult("Comment was updated!"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("deleteComment")]
        public ActionResult<string> DeleteComment([System.Web.Http.FromUri] DeleteCommentRequest request)
        {
            bool user = _userService.CheckUser(request.UserLogin, request.UserPassword);

            if (!user) return BadRequest("There is no such a user in database or password is incorrect");

            bool isLet = _userService.isAllowedToWatchProject(request.UserLogin, request.ProjectId);
            if (!isLet) return BadRequest("Not allowed to watch");

            bool isTask = _userService.isTaskOnProject(request.TaskId, request.ProjectId);
            if (!isTask) return BadRequest("Task is not at project");

            bool isComment = _userService.isCommentOnTask(request.CommentId, request.TaskId);
            if (!isComment) return BadRequest("Comment is not at task");

            bool result = _commentService.DeleteComment(request.CommentId, request.UserLogin);
            if (result)
            {
                return StatusCode(201, new JsonResult("Comment was deleted!"));
            }
            else
            {
                return BadRequest("Comment was not deleted");
            }
            
        }



    }
}
