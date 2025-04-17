using Education.Domain.Entities;
using Education.Infrastructure.Database;
using Education.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    // no need to have a private context member cause it's already inherted

    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {


        public QuestionRepository(EducationPlatformDBContext context) : base(context)
        {

        }
    
    }
}
