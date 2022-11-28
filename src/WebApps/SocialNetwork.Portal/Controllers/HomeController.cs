using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Portal.ApiServices;
using SocialNetwork.Portal.Models;
using SocialNetwork.Profile.Domain.Models;

namespace SocialNetwork.Portal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProfileApiService profileApiService;

    public HomeController(ILogger<HomeController> logger, IProfileApiService profileApiService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.profileApiService = profileApiService ?? throw new ArgumentNullException(nameof(profileApiService));
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<ProfileData> list = await profileApiService.GetProfilesAsync();
        return View(list);
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
