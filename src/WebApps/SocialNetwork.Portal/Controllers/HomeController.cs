using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Portal.Models;
using SocialNetwork.Profile.Domain.Models;

namespace SocialNetwork.Portal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<ProfileData> list = new List<ProfileData>{
            new ProfileData
            {
                Id = 1,
                Login = "Mental",
                FirstName = "Ruslan",
                LastName = "Galiev",
                Birthday = new DateTime(1985, 7, 24),
                City = "Nizhnevartovsk",
                Country = "Russian Federation"
            } 
        };
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
