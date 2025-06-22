using HelperPE.Common.Constants;
using HelperPE.Common.Converters;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Application.Services.Implementations
{
    public class ProfileServiceImpl : IProfileService
    {
        private readonly DataContext _context;

        public ProfileServiceImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<StudentProfileDTO> GetStudenProfileById(Guid id)
        {
            var student = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            return ((StudentEntity)student).ToDto();
        }
    }
}
