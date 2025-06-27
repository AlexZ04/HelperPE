using HelperPE.Persistence.Entities.Pairs;

namespace HelperPE.Persistence.Repositories
{
    public interface IPairRepository
    {
        public Task<SubjectEntity> GetSubject(Guid id);
        public Task<PairEntity> GetPair(Guid id);
    }
}
