using Education.Application.DTO_s;
using Education.Application.DTO_s.QuestionDto_s;
using Education.Domain.Entities;
using Education.Domain.Repository;

namespace Education.Application.Services.QuestionServices
{
    public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository = questionRepository;

        public async Task Add(CreateQuestionDto questionDto)
        {
            Question question = new Question
            {
                Header = questionDto.Header,
                Order = questionDto.Order,
                QuizId = questionDto.QuizId,
                CorrectAnswer = questionDto.CorrectAnswer
            };
            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                 _questionRepository.Delete(id);

                await _questionRepository.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
          
        }

        public async Task<QuestionsDTO?> GetQuestionDtoById(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);

            if (question == null) return null;

            // Map the question entity to the DTO
            var qustionDto = new QuestionsDTO
            {
                Id = question.Id,
                Header = question.Header,
                Order = question.Order,
                CorrectAnswer = question.CorrectAnswer,
                QuizId = question.QuizId
            };

            return qustionDto;
        }

        public async Task<Question?> GetQuestionById(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);

            if (question == null) return null;


            return question;
        }

        public async Task<IEnumerable<Question>> GetQuestions()
        {
            var Quistions = await _questionRepository.GetAllAsync();
            return Quistions;
        }

        public async Task<bool> Update(int Id , UpdateQuestionDto updateQuestionDto)
        {
            var question = await GetQuestionById(Id);

            if (question == null) return false;

            question.Header = updateQuestionDto.Header;
            question.Order = updateQuestionDto.Order;
            question.CorrectAnswer = updateQuestionDto.CorrectAnswer;
            try
            {
                _questionRepository.Update(question);

                await _questionRepository.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
