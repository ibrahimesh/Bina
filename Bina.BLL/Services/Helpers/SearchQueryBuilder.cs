using System.Linq;
using Bina.DAL.Models;
using Bina.BLL.DTOs.Property;
using Microsoft.EntityFrameworkCore;
using Bina.DAL.Enums;

namespace Bina.BLL.Services.Helpers
{
    public static class SearchQueryBuilder
    {
        public static IQueryable<Property> Apply(IQueryable<Property> query, PropertyFilterDto filter)
        {
            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

            if (filter.CityId.HasValue)
                query = query.Where(p => p.CityId == filter.CityId.Value);

            if (filter.DistrictId.HasValue)
                query = query.Where(p => p.DistrictId == filter.DistrictId.Value);

            if (filter.MetroId.HasValue)
                query = query.Where(p => p.MetroId == filter.MetroId.Value);

            if (filter.ListingType.HasValue)
                query = query.Where(p => p.ListingType == (ListingType)filter.ListingType.Value);

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (filter.MinArea.HasValue)
                query = query.Where(p => p.Area >= filter.MinArea.Value);

            if (filter.MaxArea.HasValue)
                query = query.Where(p => p.Area <= filter.MaxArea.Value);

            if (filter.RoomCount.HasValue)
                query = query.Where(p => p.RoomCount == filter.RoomCount.Value);

            if (filter.HasRepair.HasValue)
                query = query.Where(p => p.HasRepair == filter.HasRepair.Value);

            if (filter.HasMortgage.HasValue)
                query = query.Where(p => p.HasMortgage == filter.HasMortgage.Value);

            if (filter.IsUrgent.HasValue && filter.IsUrgent.Value)
                query = query.Where(p => p.IsUrgent == true);

            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                var lowerKeyword = filter.Keyword.ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(lowerKeyword) || p.Description.ToLower().Contains(lowerKeyword));
            }

            switch (filter.SortBy)
            {
                case "price_asc":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                case "area_asc":
                    query = query.OrderBy(p => p.Area);
                    break;
                case "area_desc":
                    query = query.OrderByDescending(p => p.Area);
                    break;
                case "date_asc":
                    query = query.OrderBy(p => p.CreatedAt);
                    break;
                case "date_desc":
                default:
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;
            }

            return query;
        }
    }
}