using Microsoft.AspNetCore.Mvc;
using Bina.Client.Models;
using Bina.Client.Models.DTOs;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Bina.Client.Controllers
{
    public class ElanController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ElanController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePropertyDto model)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _clientFactory.CreateClient("BinaApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = JsonContent.Create(model);
            var response = await client.PostAsync("Property", content);
            var responseText = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<object>>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResponse?.Errors != null && apiResponse.Errors.Count > 0)
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(apiResponse?.Message))
                {
                    ModelState.AddModelError(string.Empty, apiResponse.Message);
                }
                else
                {
                    ModelState.AddModelError("", "Elan yarad?lark?n x?ta ba? verdi.");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Elan yarad?lark?n x?ta ba? verdi. Z?hm?t olmasa yenid?n yoxlay?n.");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var client = _clientFactory.CreateClient("BinaApi");
            var response = await client.GetAsync($"Property/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<PropertyResponseDto>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiResponse?.Data != null)
                {
                    return View(apiResponse.Data);
                }
            }

            var demoListing = GetDemoListingById(id);
            if (demoListing != null)
            {
                return View(demoListing);
            }

            return RedirectToAction("Index", "Home");
        }

        private static PropertyResponseDto? GetDemoListingById(int id)
        {
            var demo = new List<PropertyResponseDto>
            {
                new PropertyResponseDto
                {
                    Id = 1001,
                    Title = "Yasamalda yeni tikili 3 otaq",
                    Price = 185000,
                    Area = 110,
                    RoomCount = 3,
                    MainImageUrl = "https://images.unsplash.com/photo-1560185008-b033106af5c3?auto=format&fit=crop&w=800&q=80",
                    CityName = "Bak?",
                    DistrictName = "Yasamal",
                    Description = "Yeni t?mirli, geni? v? i??ql? m?nzil. M?kt?b v? metroya yax?n ?razid? yerl??ir.",
                    Floor = 5,
                    TotalFloors = 16,
                    CategoryName = "M?nzil",
                    MetroName = "?n?aatç?lar",
                    CreatedAt = DateTime.UtcNow.AddHours(-3),
                    HasRepair = true,
                    HasMortgage = false,
                    IsUrgent = false,
                    IsNegotiable = true
                },
                new PropertyResponseDto
                {
                    Id = 1002,
                    Title = "N?rimanovda 2 otaql? m?nzil",
                    Price = 1200,
                    Area = 75,
                    RoomCount = 2,
                    MainImageUrl = "https://images.unsplash.com/photo-1494526585095-c41746248156?auto=format&fit=crop&w=800&q=80",
                    CityName = "Bak?",
                    DistrictName = "N?rimanov",
                    Description = "Tam ??yal? kiray? m?nzil. Ya?amaq üçün haz?r v?ziyy?td?dir.",
                    Floor = 7,
                    TotalFloors = 14,
                    CategoryName = "M?nzil",
                    MetroName = "N?riman N?rimanov",
                    CreatedAt = DateTime.UtcNow.AddHours(-8),
                    HasRepair = true,
                    HasMortgage = false,
                    IsUrgent = true,
                    IsNegotiable = false
                },
                new PropertyResponseDto
                {
                    Id = 1003,
                    Title = "X?rdalanda h?y?t evi",
                    Price = 98000,
                    Area = 130,
                    RoomCount = 4,
                    MainImageUrl = "https://images.unsplash.com/photo-1570129477492-45c003edd2be?auto=format&fit=crop&w=800&q=80",
                    CityName = "Ab?eron",
                    DistrictName = "X?rdalan",
                    Description = "H?y?tyan? sah?si olan rahat ail? evi. S?n?dl?ri qaydas?ndad?r.",
                    Floor = 1,
                    TotalFloors = 1,
                    CategoryName = "Ev",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    HasRepair = true,
                    HasMortgage = true,
                    IsUrgent = false,
                    IsNegotiable = true
                },
                new PropertyResponseDto
                {
                    Id = 1004,
                    Title = "Sumqay?tda kiray? 1 otaq",
                    Price = 450,
                    Area = 42,
                    RoomCount = 1,
                    MainImageUrl = "https://images.unsplash.com/photo-1501183638710-841dd1904471?auto=format&fit=crop&w=800&q=80",
                    CityName = "Sumqay?t",
                    DistrictName = "M?rk?z",
                    Description = "M?rk?zd? yerl???n ?lveri?li kiray? m?nzil. Kommunal xidm?tl?r yax?nl?qdad?r.",
                    Floor = 3,
                    TotalFloors = 9,
                    CategoryName = "M?nzil",
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    HasRepair = false,
                    HasMortgage = false,
                    IsUrgent = false,
                    IsNegotiable = true
                }
            };

            return demo.FirstOrDefault(x => x.Id == id);
        }
    }
}