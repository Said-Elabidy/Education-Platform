using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
    public class Quiestion
    {
        public int QuiestionId { get; set; }
        public string QuiestionTitle{ get; set; }

        public bool? UserAnswer { get; set; }

        public bool CorretAnswer { get; set; }

        public bool IsCorrect { get; set; }
       
        

    }
}
