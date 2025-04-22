using Education.Application.DTO_s;
using Education.Application.DTO_s.QuestionDto_s;
using Education.Application.Services.QuestionServices;
using Education.Domain.Entities;
using Education.Domain.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;

        // get All Questions
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestions()
        {
            var questions =await _questionService.GetQuestions();

            if (!questions.Any())
            {
                return NotFound("No Question Found");
            }

            return Ok(questions);
        }

        // get All Question By It's Id

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionsDTO>> GetQuestionById([FromRoute] int id)
        {
            var question = await _questionService.GetQuestionDtoById(id);

            if (question == null)
            {
                return NotFound($"No Question Found with Id : {id}");
            }

            return Ok(question);
        }

        // Add new Question

        [HttpPost]
        [Authorize(Roles = MyRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddQuestion([FromForm] CreateQuestionDto questionDto)
        {
           if(questionDto == null) { return BadRequest(); } 

            try
            {
                await _questionService.Add(questionDto);
                return Created();
            }
            catch
            {
                return BadRequest();
            }
        }

        // Delete Question By Id

        [HttpDelete("{questionId}")]
        [Authorize(Roles = MyRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteQuestion ([FromRoute]int QuestionId)
        {
           var IsDeleted =  await _questionService.Delete(QuestionId);

           return IsDeleted ? NoContent() : NotFound();
        }

        // Update Question

        [HttpPut("{Id}")]
        [Authorize(Roles = MyRoles.Admin)]
        public async Task<ActionResult> UpdateQuestion([FromRoute] int Id,[FromForm] UpdateQuestionDto updateQuestionDto)
        {
            var isUpdated =await _questionService.Update(Id, updateQuestionDto);

            return isUpdated ? NoContent() : NotFound();
        }

    }
}
