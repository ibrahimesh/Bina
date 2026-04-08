using Microsoft.AspNetCore.Mvc;
using Bina.Client.Models;
using Bina.Client.Models.DTOs;
using System.Text.Json;
using System.Text;

namespace Bina.Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var client = _clientFactory.CreateClient("BinaApi");
            var content = JsonContent.Create(model);
            var response = await client.PostAsync("Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                // Store token in session (basic implementation)
                var result = await response.Content.ReadAsStringAsync();
                var parsed = JsonDocument.Parse(result);
                if (parsed.RootElement.TryGetProperty("data", out JsonElement dataEle) && 
                    dataEle.TryGetProperty("token", out JsonElement tokenEle))
                {
                    HttpContext.Session.SetString("JWToken", tokenEle.GetString());
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "E-po?t v? ya ?ifr? yanl??d?r.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var client = _clientFactory.CreateClient("BinaApi");
            var content = JsonContent.Create(model);
            var response = await client.PostAsync("Auth/register", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Auth");
            }

            var errTxt = await response.Content.ReadAsStringAsync();
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<object>>(errTxt, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResponse != null)
                {
                    if (apiResponse.Errors != null && apiResponse.Errors.Count > 0)
                    {
                        foreach (var err in apiResponse.Errors)
                        {
                            ModelState.AddModelError("", err);
                        }
                        return View(model);
                    }
                    else if (!string.IsNullOrEmpty(apiResponse.Message))
                    {
                        ModelState.AddModelError("", apiResponse.Message);
                        return View(model);
                    }
                }
            } catch { }

            ModelState.AddModelError("", "Qeydiyyat zaman? x?ta ba? verdi.");
            return View(model);
        }
    }
}