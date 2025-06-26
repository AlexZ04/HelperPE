using HelperPE.Common.Models.Event;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Repositories;
using HelperPE.Persistence.Extensions;

namespace HelperPE.Application.Services.Implementations
{
    public class SportsServiceImpl : ISportsService
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        public SportsServiceImpl(
            DataContext context,
            IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<EventListModel> GetSportsOrgEventList(Guid sportsOrgId)
        {
            var sportsOrg = await _userRepository.GetSportsById(sportsOrgId);

            return new EventListModel
            {
                Events = sportsOrg.Events.Select(e => e.ToDto()).ToList(),
            };
        }
    }
}
