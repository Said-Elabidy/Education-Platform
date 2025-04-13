using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
    public class UserQuiz
    {
        public Guid UserId { get; set; }

        public int QuizId { get; set; }

        public int Score { get; set; } = 0;

        public bool IsPassed { get; set; }

        
        public Quiz Quiz { get; set; } = null!;
    }
}
