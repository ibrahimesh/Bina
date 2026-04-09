using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Bina.Client.Controllers;

public class LanguageController : Controller
{
    private static readonly HashSet<string> SupportedCultures = new(StringComparer.OrdinalIgnoreCase)
    {
        "az-AZ",
        "en-US",
        "ru-RU"
    };

    [HttpGet]
    public IActionResult SetLanguage(string culture, string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(culture) || !SupportedCultures.Contains(culture))
        {
            culture = "az-AZ";
        }

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true,
                Path = "/"
            }
        );

        var safeReturnUrl = NormalizeReturnUrl(returnUrl);
        var separator = safeReturnUrl.Contains('?') ? "&" : "?";
        return LocalRedirect($"{safeReturnUrl}{separator}culture={Uri.EscapeDataString(culture)}&ui-culture={Uri.EscapeDataString(culture)}");
    }

    private string NormalizeReturnUrl(string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(returnUrl) || !Url.IsLocalUrl(returnUrl))
        {
            return Url.Action("Index", "Home") ?? "/";
        }

        var parts = returnUrl.Split('?', 2);
        var path = parts[0];
        if (parts.Length == 1)
        {
            return path;
        }

        var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(parts[1]);
        var filtered = query
            .Where(kvp => !string.Equals(kvp.Key, "culture", StringComparison.OrdinalIgnoreCase)
                       && !string.Equals(kvp.Key, "ui-culture", StringComparison.OrdinalIgnoreCase))
            .SelectMany(kvp => kvp.Value, (kvp, value) => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(value ?? string.Empty)}")
            .ToList();

        return filtered.Count == 0 ? path : $"{path}?{string.Join("&", filtered)}";
    }
}
