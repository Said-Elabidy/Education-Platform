using Education.Application.DTO_s.QuestionDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.QuizDto_s
{
    public class GetQuizWithIcloudQuestions
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public int PassingScore { get; set; } = default(int);

        public int SectionId { get; set; }

        public int? NumOfQuestion { get; set; }
        public List<QuestionsDTO> Questions { get; set; } = new List<QuestionsDTO>();
    }
}
