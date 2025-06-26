using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using HelperPE.Persistence.Extensions;

namespace HelperPE.Application.Services.Implementations
{
    public class CuratorServiceImpl : ICuratorService
    {
        private readonly IProfileService _profileService;
        private readonly DataContext _context;

        public CuratorServiceImpl(
            IProfileService profileService,
            DataContext context)
        {
            _profileService = profileService;
            _context = context;
        }

        public async Task<UserActivitiesModel> GetUserInfo(Guid userId)
        {
            var userActivities = await _profileService.GetUserActivities(userId);

            return userActivities;
        }

        public async Task EditEventApplicationStatus(Guid eventId, Guid userId, bool approve = true)
        {
            var application = await _context.EventsAttendances
                .FirstOrDefaultAsync(a => a.StudentId == userId && a.EventId == eventId);

            if (application == null)
                throw new DirectoryNotFoundException(ErrorMessages.APPLICATION_NOT_FOUND);

            if (application.Status == EventApplicationStatus.Pending)
                throw new BadRequestException(ErrorMessages.CAN_NOT_CHANGE_FIELD);

            if (application.Status == EventApplicationStatus.Credited && approve ||
                !approve && application.Status == EventApplicationStatus.Declined)
                throw new BadRequestException(ErrorMessages.ACTION_ALREADY_DONE);

            application.Status = approve ? EventApplicationStatus.Credited 
                : EventApplicationStatus.Declined;

            await _context.SaveChangesAsync();
        }

        public async Task<FacultiesModal> GetCuratorFaculties(Guid userId)
        {
            var user = await _profileService.GetCuratorProfileById(userId);

            return new FacultiesModal
            {
                Faculties = user.Faculties,
            };
        }
    }
}
