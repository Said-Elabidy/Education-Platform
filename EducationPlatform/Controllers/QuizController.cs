﻿using Education.Application.DTO_s.QuizDto_s;
using Education.Application.DTO_s.SectionDTO_s;
using Education.Application.Services.QuizServices;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetQuizzes();

            return quizzes.Any() ? Ok(quizzes) : NotFound("No quizzes found");
        }
        [HttpGet("bySection/{SectionId:int}")]
        public async Task<ActionResult<GetQuizeDTO?>> GetQuizBySectionId(int SectionId)
        {
            var quiz = await _quizService.GetQuieBySectionId(SectionId);
            if (quiz != null)
                return Ok(quiz);
            return NotFound("No Quiz in this Section");

        }   

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Quiz>> AddQuiz([FromBody]AddQuizDto addQuizDto)

        {
            try
            {
                var newQuiz = await _quizService.Add(addQuizDto);

                if (newQuiz is null)
                    return BadRequest("Quiz Was Not Saved Correctly");

                return CreatedAtAction(nameof(GetQuizById), new { id = newQuiz.Id }, newQuiz);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add quiz: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Quiz>> GetQuizById(int id)
        {
            var quiz = await _quizService.GetQuizById(id);

            return quiz != null ? Ok(quiz) : NotFound($"Quiz with ID : {id} not found");
        }

        [HttpGet("{id}/questions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetQuizWithIcloudQuestions?>> GetQuizWithQuestions(int id)
        {
            var quiz = await _quizService.GetQuizWithQuestions(id);

            return quiz != null ? Ok(quiz) : NotFound($"Quiz with ID : {id} not found");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var isDeleted = await _quizService.Delete(id);

            return isDeleted ? NoContent() : BadRequest($"Failed to delete quiz with ID : {id}");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateQuiz([FromRoute]int id,[FromBody] UpdateQuizDto updateQuizDto)

        {
            var isUpdated = await _quizService.Update(id, updateQuizDto);

            return isUpdated ? Ok("Quiz updated succesfully") : NotFound($"Quiz with ID : {id} not found");
        }
    }
}