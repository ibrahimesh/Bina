using Microsoft.AspNetCore.Mvc;
using Bina.Client.Models;
using Bina.Client.Models.DTOs;
using System.Text.Json;

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
        public async Task<IActionResult> Index(string query)
        {
            var properties = new List<PropertyListDto>();
            if (!string.IsNullOrEmpty(query))
            {
                var client = _clientFactory.CreateClient("BinaApi");
                var response = await client.GetAsync($"Property?search={query}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<PagedResult<PropertyListDto>>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (apiResponse != null && apiResponse.Data != null)
                    {
                        properties = apiResponse.Data.Items?.ToList() ?? new List<PropertyListDto>();
                    }
                }
            }

            ViewData["Query"] = query;
            return View(properties);
        }
    }
}