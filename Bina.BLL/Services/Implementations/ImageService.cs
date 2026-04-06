using Bina.BLL.DTOs.Common;
using Bina.BLL.Services.Contracts;
using Bina.DAL.Models;
using Bina.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bina.BLL.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IRepository<PropertyImage> _imageRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IWebHostEnvironment _env;

        public ImageService(IRepository<PropertyImage> imageRepository, IPropertyRepository propertyRepository, IWebHostEnvironment env)
        {
            _imageRepository = imageRepository;
            _propertyRepository = propertyRepository;
            _env = env;
        }

        public async Task<ApiResponse<string>> UploadAsync(IFormFile file, int propertyId)
        {
            try
            {
                var property = await _propertyRepository.GetByIdAsync(propertyId);
                if (property == null) return ApiResponse<string>.Fail("?mlak tap?lmad?");

                var uploadsFolder = Path.Combine(_env.WebRootPath, "images", propertyId.ToString());
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var imageUrl = $"/images/{propertyId}/{uniqueFileName}";

                var imageEntity = new PropertyImage
                {
                    PropertyId = propertyId,
                    Url = imageUrl,
                    IsMain = !property.Images.Any(),
                    OrderIndex = property.Images.Count
                };

                await _imageRepository.AddAsync(imageEntity);
                await _imageRepository.SaveChangesAsync();

                return ApiResponse<string>.Ok(imageUrl);
            }
            catch(Exception ex)
            {
                return ApiResponse<string>.Fail("Fayl yükl?n?rk?n x?ta", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int imageId, int userId)
        {
            try 
            {
                var image = await _imageRepository.GetByIdAsync(imageId);
                if (image == null) return ApiResponse<bool>.Fail("??kil tap?lmad?");

                var property = await _propertyRepository.GetByIdAsync(image.PropertyId);
                if (property == null || property.UserId != userId) return ApiResponse<bool>.Fail("S?lahiyy?t yoxdur");

                var physicalPath = Path.Combine(_env.WebRootPath, image.Url.TrimStart('/'));
                if (File.Exists(physicalPath))
                {
                    File.Delete(physicalPath);
                }

                _imageRepository.Remove(image);
                await _imageRepository.SaveChangesAsync();

                return ApiResponse<bool>.Ok(true);
            }
            catch(Exception ex)
            {
                return ApiResponse<bool>.Fail("X?ta ba? verdi", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> SetMainAsync(int imageId, int propertyId)
        {
            try
            {
                var property = await _propertyRepository.GetByIdAsync(propertyId);
                if (property == null) return ApiResponse<bool>.Fail("?mlak tap?lmad?");

                var targetImage = property.Images.FirstOrDefault(i => i.Id == imageId);
                if (targetImage == null) return ApiResponse<bool>.Fail("??kil bu ?mlaka aid deyil");

                foreach (var img in property.Images)
                {
                    img.IsMain = img.Id == imageId;
                    _imageRepository.Update(img);
                }

                await _imageRepository.SaveChangesAsync();
                return ApiResponse<bool>.Ok(true);
            }
            catch(Exception ex)
            {
                return ApiResponse<bool>.Fail("X?ta", ex.Message);
            }
        }
    }
}