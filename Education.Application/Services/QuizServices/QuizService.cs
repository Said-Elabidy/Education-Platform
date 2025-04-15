using Education.Application.DTO_s.QuizDto_s;
using Education.Domain.Entities;
using Education.Domain.Repository;

namespace Education.Application.Services.QuizServices
{
    public class QuizService(IQuizRepository quizRepository) : IQuizService
    {
        private readonly IQuizRepository _quizRepository = quizRepository;

        public async Task<Quiz> Add(AddQuizDto addQuizDto)
        {
            Quiz quiz = new Quiz
            {
                Title = addQuizDto.Title,
                PassingScore = addQuizDto.PassingScore,
                SectionId = addQuizDto.SectionId,
                NumOfQuestion = 0
            };

            await _quizRepository.AddAsync(quiz);
            await _quizRepository.SaveChangesAsync();
            return quiz; // Return the created quiz with ID
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await _quizRepository.Delete(id);
                await _quizRepository.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
            

        }

        public async Task<Quiz?> GetQuizById(int id)
        {
            Quiz? quiz = await _quizRepository.GetByIdAsync(id);

            return quiz;
        }

        public async Task<Quiz?> GetQuizWithQuestions(int Id)
        {
            Quiz? quiz = await _quizRepository.GetQuizIncludeQuestionsAsync(Id);

            return quiz;
        }

        public async Task<IEnumerable<Quiz>> GetQuizzes()
        {
            var quizzes = await _quizRepository.GetAllAsync();

            return quizzes;
        }

        public async Task<bool> Update(int id, UpdateQuizDto updateQuizDto)
        {
            var quiz = await GetQuizById(id);

            if (quiz == null)
                return false;

            if (!string.IsNullOrEmpty(updateQuizDto.Title))
            {
                quiz.Title = updateQuizDto.Title;
            }

            if (updateQuizDto.PassingScore.HasValue)
            {
                quiz.PassingScore = updateQuizDto.PassingScore.Value;
            }


            try
            {
                _quizRepository.Update(quiz);
                await _quizRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
