using Microsoft.AspNetCore.Mvc;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.Mvc.Controllers
{
    public class PlotController : Controller
    {
        private readonly IPlotService _plotService;
        private readonly IHomeownerService _homeownerService;

        private readonly ILogger<PlotController> _logger;

        public PlotController(ILogger<PlotController> logger, IPlotService plotService, IHomeownerService homeownerService)
        {
            _logger = logger;
            _plotService = plotService;
            _homeownerService = homeownerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Plot> plots = await _plotService.GetPlots();

            List<PlotVM> viewModel = new();
            foreach (Plot p in plots)
            {
                PlotVM plotVM = new();
                plotVM.Map(p);
                plotVM.HomeownerName = p.HomeownerId.HasValue ?
                    (await _homeownerService.GetHomeowner((int)p.HomeownerId)).FullName : "No Owner";

                viewModel.Add(plotVM);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            PlotVM viewModel = new();
            viewModel.Map(await _plotService.GetPlot(id));
            viewModel.HomeownerName = string.IsNullOrEmpty(viewModel.HomeownerID) ?
                 "No Owner" : (await _homeownerService.GetHomeowner(int.Parse(viewModel.HomeownerID))).FullName;

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> FeeDetails(int id)
        {
            ViewBag.PlotId = id;

            List<Fee> fees = await _plotService.GetFeesForPlot(id);
            List<FeeVM> viewModel = new();

            if (fees.Count > 0)
            {
                foreach (Fee f in fees)
                {
                    FeeVM feeVM = new FeeVM();
                    feeVM.Map(f);

                    viewModel.Add(feeVM);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrUpdate(int? id)
        {
            // Get a list of available homeowners to select from
            ViewBag.Homeowners = (await _homeownerService.GetHomeowners())
                                        .Select(x => new { x.HomeownerId, x.FullName });

            if (id.HasValue)
            {
                PlotVM viewModel = new();
                viewModel.Map(await _plotService.GetPlot(id.Value));

                return View(viewModel);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(PlotVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Convert string id value to nullable int
            int? homeownerId = string.IsNullOrEmpty(viewModel.HomeownerID) ? null : int.Parse(viewModel!.HomeownerID);

            if (string.IsNullOrEmpty(viewModel.PlotID))
            {
                //Create
                await _plotService.CreatePlot(viewModel.MapOut(), homeownerId);
            }
            else
            {
                //Update
                await _plotService.UpdatePlot(viewModel.MapOut(), homeownerId);
            }

            return RedirectToAction("Index");
        }
    }
}
