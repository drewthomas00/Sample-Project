using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.Mvc.Models;

namespace VanderbiltFarms.Web.Mvc.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly IHealthCheckService _healthCheckService;

        private readonly ILogger<WelcomeController> _logger;

        public WelcomeController(ILogger<WelcomeController> logger, IHealthCheckService healthCheckService)
        {
            _logger = logger;
            _healthCheckService = healthCheckService;
        }

        public async Task<IActionResult> Index()
        {
            return await _healthCheckService.HealthCheck() ? View() : Error();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}