using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
    public class UserQuiz
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int QuizId { get; set; }

        public int Score { get; set; } = 0;

        public bool IsPassed { get; set; }

        
        public Quiz Quiz { get; set; } = null!;

        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
