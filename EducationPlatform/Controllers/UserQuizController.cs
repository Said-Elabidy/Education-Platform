using Education.Application.Services.UserQuizServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserQuizController : ControllerBase
    {
        private readonly IUserQuizServices _userQuizServices;

        public UserQuizController(IUserQuizServices userQuizServices)
        {
            _userQuizServices = userQuizServices;
        }
        [HttpGet("UserQuiz")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuizzesByUserId()
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");
            var quizzes = await _userQuizServices.GetQuizzesByUserIdAsync(userId);
            return Ok(quizzes);
        }
        [HttpGet("quiz/{quizId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByQuizId(int quizId)
        {
            var users = await _userQuizServices.GetUsersByQuizIdAsync(quizId);
            return Ok(users);
        }
        [HttpPut("{quizId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]



        public async Task<IActionResult> UpdateQuizScore( int quizId, [FromBody] int score)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            var result = await _userQuizServices.UpdateQuizScoreAsync(userId, quizId, score);
            if (!result)
                return BadRequest("Failed to update quiz score.");
            return Ok("Quiz score updated successfully.");
        }


    }
}
