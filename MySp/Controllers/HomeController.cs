using System.Diagnostics;
using System.Security.Cryptography.Pkcs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySp.Models;

namespace MySp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {        
        return View();
    }

    public IActionResult FromMCA(int autoOpen = 0)
    {
        if (User?.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Details");
        }
        else
        {
            return View();
        }
    }

    [Authorize]
    public IActionResult Details()
    {
        return View();
    }

    [Authorize]
    public IActionResult CloseMe()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
