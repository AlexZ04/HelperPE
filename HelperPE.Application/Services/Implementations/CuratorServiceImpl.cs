using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Attendances;
using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Profile;

namespace HelperPE.Application.Services.Implementations
{
    public class CuratorServiceImpl : ICuratorService
    {
        private readonly IProfileService _profileService;

        public CuratorServiceImpl(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<UserActivitiesModel> GetUserInfo(Guid userId)
        {
            var userActivities = await _profileService.GetUserActivities(userId);

            return userActivities;
        }
    }
}
