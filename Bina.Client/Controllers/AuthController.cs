using Microsoft.AspNetCore.Mvc;
using Bina.Client.Models;
using Bina.Client.Models.DTOs;
using System.Text.Json;

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

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var parsed = JsonDocument.Parse(result);
                if (parsed.RootElement.TryGetProperty("data", out JsonElement dataEle) &&
                    dataEle.TryGetProperty("token", out JsonElement tokenEle))
                {
                    HttpContext.Session.SetString("JWToken", tokenEle.GetString() ?? string.Empty);
                }

                return RedirectToAction("Index", "Home");
            }

            AddApiErrorsToModelState(result, "E-poçt v? ya ?ifr? yanl??d?r.");
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
            AddApiErrorsToModelState(errTxt, "Qeydiyyat zaman? x?ta ba? verdi.");
            return View(model);
        }

        private void AddApiErrorsToModelState(string responseText, string fallbackMessage)
        {
            try
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<object>>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResponse?.Errors != null && apiResponse.Errors.Count > 0)
                {
                    foreach (var err in apiResponse.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err);
                    }
                    return;
                }

                if (!string.IsNullOrWhiteSpace(apiResponse?.Message))
                {
                    ModelState.AddModelError(string.Empty, apiResponse.Message);
                    return;
                }
            }
            catch
            {
                // ignored, fallback to parsing ValidationProblemDetails shape
            }

            try
            {
                using var doc = JsonDocument.Parse(responseText);
                if (doc.RootElement.TryGetProperty("errors", out var errorsElement) && errorsElement.ValueKind == JsonValueKind.Object)
                {
                    var any = false;
                    foreach (var prop in errorsElement.EnumerateObject())
                    {
                        if (prop.Value.ValueKind != JsonValueKind.Array) continue;

                        foreach (var item in prop.Value.EnumerateArray())
                        {
                            var msg = item.GetString();
                            if (!string.IsNullOrWhiteSpace(msg))
                            {
                                ModelState.AddModelError(string.Empty, msg);
                                any = true;
                            }
                        }
                    }

                    if (any) return;
                }
            }
            catch
            {
                // ignored
            }

            ModelState.AddModelError(string.Empty, fallbackMessage);
        }
    }
}