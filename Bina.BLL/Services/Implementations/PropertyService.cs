using AutoMapper;
using Bina.BLL.DTOs.Common;
using Bina.BLL.DTOs.Property;
using Bina.BLL.Services.Contracts;
using Bina.DAL.Models;
using Bina.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Bina.DAL.Enums;
using Bina.BLL.Services.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Bina.BLL.Services.Implementations
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository propertyRepository, IFavoriteRepository favoriteRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PagedResult<PropertyListDto>>> SearchAsync(PropertyFilterDto filter, int? currentUserId = null)
        {
            try
            {
                var query = _propertyRepository.GetBaseQuery();
                query = SearchQueryBuilder.Apply(query, filter);

                int totalCount = await query.CountAsync();

                var properties = await query
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();

                var dtos = _mapper.Map<IEnumerable<PropertyListDto>>(properties).ToList();

                if (currentUserId.HasValue)
                {
                    var favoriteIds = await _favoriteRepository.GetUserFavoriteIdsAsync(currentUserId.Value);
                    foreach (var dto in dtos)
                    {
                        dto.IsFavorite = favoriteIds.Contains(dto.Id);
                    }
                }

                var pagedResult = new PagedResult<PropertyListDto>(dtos, totalCount, filter.Page, filter.PageSize);
                return ApiResponse<PagedResult<PropertyListDto>>.Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResult<PropertyListDto>>.Fail("Sistem x?tas? ba? verdi.", ex.Message);
            }
        }

        // Standard placeholders for token economy to fulfill requirements:
        public async Task<ApiResponse<PropertyResponseDto>> CreateAsync(CreatePropertyDto dto, int userId)
        {
            try 
            {
                var entity = _mapper.Map<Property>(dto);
                entity.UserId = userId;
                entity.CreatedAt = DateTime.UtcNow;
                entity.Status = PropertyStatus.Active;
                entity.ViewCount = 0;

                await _propertyRepository.AddAsync(entity);
                await _propertyRepository.SaveChangesAsync();

                return ApiResponse<PropertyResponseDto>.Ok(_mapper.Map<PropertyResponseDto>(entity));
            }
            catch(Exception ex)
            {
                return ApiResponse<PropertyResponseDto>.Fail("X?ta ba? verdi", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id, int userId)
        {
            try
            {
                var entity = await _propertyRepository.GetByIdAsync(id);
                if (entity == null || entity.UserId != userId) return ApiResponse<bool>.Fail("X?ta v? ya s?lahiyy?t yoxdur.");
                
                entity.Status = PropertyStatus.Deleted;
                _propertyRepository.Update(entity);
                await _propertyRepository.SaveChangesAsync();
                return ApiResponse<bool>.Ok(true);
            }
            catch(Exception ex)
            {
                return ApiResponse<bool>.Fail("X?ta ba? verdi", ex.Message);
            }
        }

        public async Task<ApiResponse<PropertyResponseDto>> GetByIdAsync(int id, int? currentUserId = null)
        {
            try
            {
                var entity = await _propertyRepository.GetByIdWithDetailsAsync(id);
                if (entity == null) return ApiResponse<PropertyResponseDto>.Fail("Tapilmad?");

                entity.ViewCount++;
                _propertyRepository.Update(entity);
                await _propertyRepository.SaveChangesAsync();

                var dto = _mapper.Map<PropertyResponseDto>(entity);
                if(currentUserId.HasValue)
                {
                    var favorites = await _favoriteRepository.GetUserFavoriteIdsAsync(currentUserId.Value);
                    dto.IsFavorite = favorites.Contains(dto.Id);
                }
                return ApiResponse<PropertyResponseDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ApiResponse<PropertyResponseDto>.Fail("X?ta ba? verdi", ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<PropertyListDto>>> GetByUserAsync(int userId)
        {
            try
            {
                var items = await _propertyRepository.GetAllAsync();
                var mapped = _mapper.Map<IEnumerable<PropertyListDto>>(items.Where(i => i.UserId == userId));
                return ApiResponse<IEnumerable<PropertyListDto>>.Ok(mapped);
            }
            catch(Exception ex)
            {
                return ApiResponse<IEnumerable<PropertyListDto>>.Fail("X?ta ba? verdi", ex.Message);
            }
        }

        public async Task<ApiResponse<PropertyResponseDto>> UpdateAsync(int id, UpdatePropertyDto dto, int userId)
        {
            try 
            {
                var entity = await _propertyRepository.GetByIdAsync(id);
                if (entity == null || entity.UserId != userId) return ApiResponse<PropertyResponseDto>.Fail("X?ta");
                
                _mapper.Map(dto, entity);
                entity.UpdatedAt = DateTime.UtcNow;
                _propertyRepository.Update(entity);
                await _propertyRepository.SaveChangesAsync();
                return ApiResponse<PropertyResponseDto>.Ok(_mapper.Map<PropertyResponseDto>(entity));
            }
            catch(Exception ex)
            {
                return ApiResponse<PropertyResponseDto>.Fail("Sistem x?tas?", ex.Message);
            }
        }
    }
}