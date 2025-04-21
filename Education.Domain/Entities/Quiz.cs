using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
    public class Quiz:BaseModal
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public int PassingScore { get; set; } = default(int);

        public int SectionId { get; set; }

        public int? NumOfQuestion {  get; set; } 

        public IEnumerable<Question>? Questions { get; set; } = new List<Question>();

        public Section Section { get; set; } = null!;

        public IEnumerable<UserQuiz> UserQuizzes { get; set; } = new List<UserQuiz>();
    }
}
