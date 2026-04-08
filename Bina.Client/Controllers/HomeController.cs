using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bina.Client.Models;
using Bina.Client.Models.DTOs;
using System.Text.Json;

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
            if (apiResponse != null && apiResponse.Data != null)
            {
                properties = apiResponse.Data.Items?.ToList() ?? new List<PropertyListDto>();
            }
        }

        return View(properties);
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
