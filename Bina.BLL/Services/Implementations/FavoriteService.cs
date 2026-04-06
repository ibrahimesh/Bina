using Bina.BLL.DTOs.Property;
using Bina.BLL.DTOs.Common;
using Bina.BLL.Services.Contracts;
using Bina.DAL.Models;
using Bina.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System;

namespace Bina.BLL.Services.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public FavoriteService(IFavoriteRepository favoriteRepository, IPropertyRepository propertyRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> ToggleAsync(int userId, int propertyId)
        {
            try
            {
                var favorites = await _favoriteRepository.GetAllAsync();
                var favorite = favorites.FirstOrDefault(f => f.UserId == userId && f.PropertyId == propertyId);

                if (favorite != null)
                {
                    _favoriteRepository.Remove(favorite);
                }
                else
                {
                    var newFav = new Favorite { UserId = userId, PropertyId = propertyId, CreatedAt = DateTime.UtcNow };
                    await _favoriteRepository.AddAsync(newFav);
                }
                
                await _favoriteRepository.SaveChangesAsync();
                return ApiResponse<bool>.Ok(true);
            }
            catch(Exception ex)
            {
                return ApiResponse<bool>.Fail("X?ta ba? verdi", ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<PropertyListDto>>> GetUserFavoritesAsync(int userId)
        {
            try
            {
                var favorites = await _favoriteRepository.GetAllAsync();
                var userFavs = favorites.Where(f => f.UserId == userId).Select(f => f.PropertyId).ToList();

                var properties = await _propertyRepository.GetAllAsync();
                var favoriteProperties = properties.Where(p => userFavs.Contains(p.Id));

                var dtos = _mapper.Map<IEnumerable<PropertyListDto>>(favoriteProperties);
                foreach (var dto in dtos) dto.IsFavorite = true;

                return ApiResponse<IEnumerable<PropertyListDto>>.Ok(dtos);
            }
            catch(Exception ex)
            {
                return ApiResponse<IEnumerable<PropertyListDto>>.Fail("Sistem x?ta verdi", ex.Message);
            }
        }
    }
}