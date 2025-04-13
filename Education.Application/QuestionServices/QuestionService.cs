using Education.Domain.Entities;
using Education.Domain.Repository;

namespace Education.Application.QuestionServices
{
    public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository = questionRepository;

        public async Task Add(Question question)
        {
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

        public Task<Question> GetQuestionById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Question>> GetQuestions()
        {
            var Quistions = await _questionRepository.GetAllAsync();
            return Quistions;
        }

        public async Task<bool> Update(Question question)
        {
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
