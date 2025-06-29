using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities;
using HelperPE.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Application.Services.Implementations
{
    public class AvatarServiceImpl : IAvatarService
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        public AvatarServiceImpl(DataContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<GuidResponseModel> AddAvatar(Guid userId, IFormFile avatar) //? CredentialsException
        {
            var user = await _userRepository.GetUserById(userId);
            if(user.Avatar != null) { throw new BadRequestException(ErrorMessages.YOU_ALREADY_HAVE_AVATAR); }
            user.Avatar = await CreateFile(avatar);
            await _context.SaveChangesAsync();
            return new GuidResponseModel 
                {
                    Id = (user.Avatar.Id) 
                };
        }

        public async Task<GuidResponseModel> ChangeAvatar(Guid userId, IFormFile avatar)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user.Avatar == null) { throw new BadRequestException(ErrorMessages.YOU_DONT_HAVE_AVATAR); }

            user.Avatar = await CreateFile(avatar);
            await _context.SaveChangesAsync();
            return new GuidResponseModel
            {
                Id = (user.Avatar.Id)
            };
        }

        public async Task DeleteAvatar(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user.Avatar == null) { throw new BadRequestException(ErrorMessages.YOU_DONT_HAVE_AVATAR); }
            user.Avatar = null;
            await _context.SaveChangesAsync();
        }

        public async Task<(byte[]? Content, string ContentType, string FileName)> GetFile(Guid fileId)
        {

            var fileDB = await _context.Files.FindAsync(fileId);

            if (fileDB == null)
            {
                throw new BadRequestException(ErrorMessages.INVALID_CREDENTIALS);
            }

            string filePath = fileDB.Path;
            if (!System.IO.File.Exists(filePath))
            {
                throw new BadRequestException(ErrorMessages.INVALID_CREDENTIALS);
            }
            return (await System.IO.File.ReadAllBytesAsync(filePath), GetContentTypeFromFileName(fileDB.Name), fileDB.Name);
        }



        private string GetContentTypeFromFileName(string fileName)
        {
            string? extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            //string extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            switch (extension)
            {
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                default: return "application/octet-stream";
            }
        }


        private async Task<FileEntity> CreateFile(IFormFile file)
        {
            if (!Directory.Exists("/FilesPe"))
            { Directory.CreateDirectory("/FilesPe"); }

            if (!IsValidImageHeader(file))
            {
                throw new BadRequestException(ErrorMessages.INVALID_FILE_TYPE);
            }

            Guid fileId = Guid.NewGuid();
            string fileName = fileId.ToString() + file.FileName;

            string path = "/FilesPe/" + fileName;

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            FileEntity newFile = new FileEntity { Id = fileId, Name = fileName, Path = path };
            _context.Files.Add(newFile);
            await _context.SaveChangesAsync();
            return newFile;
        }

        private bool IsValidImageHeader(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {

                var pngHeader = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
                var jpgHeader = new byte[] { 0xFF, 0xD8, 0xFF };


                byte[] buffer = new byte[8];

                /*stream.ReadExactly(buffer);*/ stream.Read(buffer, 0, buffer.Length);


                if (StartsWith(buffer, pngHeader))
                {
                    return true;
                }

                if (StartsWith(buffer, jpgHeader))
                {
                    return true;
                }

                return false;
            }
        }

        private bool StartsWith(byte[] buffer, byte[] header)
        {
            if (buffer.Length < header.Length)
            {
                return false;
            }

            for (int i = 0; i < header.Length; i++)
            {
                if (buffer[i] != header[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
