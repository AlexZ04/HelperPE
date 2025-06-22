using HelperPE.Common.Models.Profile;

namespace HelperPE.Application.Services
{
    public interface IProfileService
    {
        public Task<StudentProfileDTO> GetStudenProfileById(Guid id);
    }
}
