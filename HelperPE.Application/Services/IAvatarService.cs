using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Application.Services
{
    public interface IAvatarService
    {
        public Task<Guid> AddAvatar(Guid userId ,IFormFile avatar);
        public Task DeleteAvatar(Guid userId);
        public Task<Guid> ChangeAvatar(Guid userId, IFormFile avatar);
        public Task<(byte[]? Content, string ContentType, string FileName)> GetFile(Guid fileId);
    }
}
