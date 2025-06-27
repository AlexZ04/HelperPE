using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Pairs;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Persistence.Repositories.Implementations
{
    public class PairRepositoryImpl : IPairRepository
    {
        private readonly DataContext _context;

        public PairRepositoryImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<SubjectEntity> GetSubject(Guid id)
        {
            var subject = await _context.Subjects
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.Id == id);

            return subject ?? throw new NotFoundException(ErrorMessages.SUBJECT_NOT_FOUND);
        }
    }
}
