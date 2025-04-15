using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface ISectionRepository:IGenericRepository<Section>
    {
        Task<IEnumerable<Section>> getAllByCourseId(int courseId);
    }
}
