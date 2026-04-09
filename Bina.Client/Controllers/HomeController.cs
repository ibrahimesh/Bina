using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bina.Client.Models;
using Bina.Client.Models.DTOs;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Bina.Client.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public HomeController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _clientFactory.CreateClient("BinaApi");
        var response = await client.GetAsync("Property");

        var properties = new List<PropertyListDto>();
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<PagedResult<PropertyListDto>>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (apiResponse?.Data?.Items != null)
            {
                properties = apiResponse.Data.Items.ToList();
            }
        }

        var token = HttpContext.Session.GetString("JWToken");
        if (!string.IsNullOrWhiteSpace(token))
        {
            var myClient = _clientFactory.CreateClient("BinaApi");
            myClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var myResponse = await myClient.GetAsync("Property/my");

            if (myResponse.IsSuccessStatusCode)
            {
                var myJson = await myResponse.Content.ReadAsStringAsync();
                var myApiResponse = JsonSerializer.Deserialize<ApiResponse<IEnumerable<PropertyListDto>>>(myJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var myProperties = myApiResponse?.Data?.ToList() ?? new List<PropertyListDto>();

                foreach (var myProperty in myProperties)
                {
                    if (properties.All(p => p.Id != myProperty.Id))
                    {
                        properties.Insert(0, myProperty);
                    }
                }
            }
        }

        properties = properties
            .OrderByDescending(x => x.CreatedAt)
            .ToList();

        if (!properties.Any())
        {
            properties = GetDemoListings();
        }

        return View(properties);
    }

    private static List<PropertyListDto> GetDemoListings()
    {
        return new List<PropertyListDto>
        {
            new PropertyListDto
            {
                Id = 1001,
                Title = "Yasamalda yeni tikili 3 otaq",
                Price = 185000,
                Area = 110,
                RoomCount = 3,
                MainImageUrl = "https://images.unsplash.com/photo-1560185008-b033106af5c3?auto=format&fit=crop&w=800&q=80",
                CityName = "Bak?",
                DistrictName = "Yasamal",
                CreatedAt = DateTime.UtcNow.AddHours(-3)
            },
            new PropertyListDto
            {
                Id = 1002,
                Title = "N?rimanovda 2 otaql? m?nzil",
                Price = 1200,
                Area = 75,
                RoomCount = 2,
                MainImageUrl = "https://images.unsplash.com/photo-1494526585095-c41746248156?auto=format&fit=crop&w=800&q=80",
                CityName = "Bak?",
                DistrictName = "N?rimanov",
                CreatedAt = DateTime.UtcNow.AddHours(-8)
            },
            new PropertyListDto
            {
                Id = 1003,
                Title = "X?rdalanda h?y?t evi",
                Price = 98000,
                Area = 130,
                RoomCount = 4,
                MainImageUrl = "https://images.unsplash.com/photo-1570129477492-45c003edd2be?auto=format&fit=crop&w=800&q=80",
                CityName = "Ab?eron",
                DistrictName = "X?rdalan",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new PropertyListDto
            {
                Id = 1004,
                Title = "Sumqay?tda kiray? 1 otaq",
                Price = 450,
                Area = 42,
                RoomCount = 1,
                MainImageUrl = "https://images.unsplash.com/photo-1501183638710-841dd1904471?auto=format&fit=crop&w=800&q=80",
                CityName = "Sumqay?t",
                DistrictName = "M?rk?z",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            }
        };
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
