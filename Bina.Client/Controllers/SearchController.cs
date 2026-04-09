using Microsoft.AspNetCore.Mvc;
using Bina.Client.Models;
using Bina.Client.Models.DTOs;
using System.Text.Json;
using System.Web;

namespace Bina.Client.Controllers
{
    public class SearchController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public SearchController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? query, decimal? minPrice, decimal? maxPrice, int? roomCount, int? listingType)
        {
            var properties = new List<PropertyListDto>();
            var hasFilter = !string.IsNullOrWhiteSpace(query) || minPrice.HasValue || maxPrice.HasValue || roomCount.HasValue || listingType.HasValue;

            if (hasFilter)
            {
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                if (!string.IsNullOrWhiteSpace(query)) queryString["keyword"] = query;
                if (minPrice.HasValue) queryString["minPrice"] = minPrice.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                if (maxPrice.HasValue) queryString["maxPrice"] = maxPrice.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                if (roomCount.HasValue) queryString["roomCount"] = roomCount.Value.ToString();
                if (listingType.HasValue) queryString["listingType"] = listingType.Value.ToString();

                var client = _clientFactory.CreateClient("BinaApi");
                var response = await client.GetAsync($"Property?{queryString}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<PagedResult<PropertyListDto>>>(
                        jsonString,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (apiResponse?.Data?.Items != null)
                    {
                        properties = apiResponse.Data.Items.ToList();
                    }
                }
            }

            ViewData["Query"] = query;
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["RoomCount"] = roomCount;
            ViewData["ListingType"] = listingType;
            ViewData["HasFilter"] = hasFilter;

            return View(properties);
        }
    }
}