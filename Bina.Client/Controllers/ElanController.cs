using Microsoft.AspNetCore.Mvc;
using Bina.Client.Models;
using Bina.Client.Models.DTOs;
using System.Text.Json;
using System.Text;
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

            // Using purely dummy values for demo, just passing the required JSON to the API. 
            // In reality, we need full form.
            var content = JsonContent.Create(model);
            var response = await client.PostAsync("Property", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Elan yarad?lark?n x?ta ba? verdi. (B?lk? b?t?n sah?l?ri doldurmam?s?n?z v? ya avtorizasiya yanl??d?r)");
            return View(model);
        }
    }
}