using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Application.Services.Implementations
{
    public class AdminServiceImpl : IAdminService
    {
        private readonly DataContext _context;
        private readonly IProfileService _profileServiceImpl;
        public AdminServiceImpl(DataContext context, IProfileService profileServiceImpl)
        {
            _context = context;
            _profileServiceImpl = profileServiceImpl;
        }

        public async Task<List<CuratorProfileDTO>> GetCurators()
        {
            var curators = await _profileServiceImpl.GetCurators();
            return (curators);
        }

        public async Task<List<TeacherProfileDTO>> GetTeachers()
        {
            var teachers = await _profileServiceImpl.GetTeachers();
            return (teachers);
        }
    }
}
