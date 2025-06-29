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

        public async Task<PairEntity> GetPair(Guid id)
        {
            var pair = await _context.Pairs
                .Include(p => p.Teacher)
                .Include(p => p.Attendances)
                    .ThenInclude(a => a.Student)
                        .ThenInclude(s => s.Faculty)
                .Include(p => p.Attendances)
                    .ThenInclude(a => a.Student)
                        .ThenInclude(s => s.Avatar)
                .Include(p => p.Subject)
                .FirstOrDefaultAsync(p => p.PairId == id);

            return pair ?? throw new NotFoundException(ErrorMessages.PAIR_NOT_FOUND);
        }
    }
}
