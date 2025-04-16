using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface ISectionRepository<TSection> : IGenericRepository<Section>
    {
        Task<IEnumerable<TSection>> getAllByCourseId(int courseId);
        Task<TSection?> getBySectionId(int sectionId);
    }
}