using Education.Application.DTO_s;
using Education.Application.QuestionServices;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;

        // get All Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestions()
        {
            var questions =await _questionService.GetQuestions();

            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

        // get All Question By It's Id

        [HttpGet("{Id}")]
        public async Task<ActionResult<Question>> GetQuestionById([FromRoute] int Id)
        {
            var question = await _questionService.GetQuestionById(Id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // Add new Question

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddQuestion([FromBody] CreateQuestionDto questionDto)
        {
            Question question = new Question
            {
                Header = questionDto.Header,
                Order = questionDto.Order,
                QuizId = questionDto.QuizId,
                CorrectAnswer = questionDto.CorrectAnswer
            };

            try
            {
                await _questionService.Add(question);
                return Created();
            }
            catch
            {
                return BadRequest();
            }
        }

        // Delete Question By Id

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteQuestion ([FromRoute]int QuestionId)
        {
           var IsDeleted =  await _questionService.Delete(QuestionId);

           return IsDeleted ? NoContent() : NotFound();
        }

        // Update Question

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateQuestion([FromRoute]int Id,UpdateQuestionDto updateQuestionDto)
        {
            var question =await _questionService.GetQuestionById(Id);

            if(question == null) return NotFound();

            question.Header = updateQuestionDto.Header;
            question.Order = updateQuestionDto.Order;
            question.CorrectAnswer = updateQuestionDto.CorrectAnswer;

            var isUpdated =await _questionService.Update(question);

            return isUpdated ? NoContent() : NotFound();
        }

    }
}
