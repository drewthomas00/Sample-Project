using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Shared.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace VanderbiltFarms.Web.BlazorWASMCoreHosted.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PlotController : ControllerBase
    {
        private readonly ILogger<PlotController> _logger;
        private IPlotService _service;

        public PlotController(ILogger<PlotController> logger, IPlotService service)
        {
            _logger = logger;
            _service = service;
        }

        // Read
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var outputList = new List<PlotVM>();
            List<Plot> temp = await _service.GetPlots();
            if (temp == null)
            {
                return NotFound("No plots found");
            }
            foreach (Plot p in temp)
            {
                PlotVM plotVM = new PlotVM();
                plotVM.Map(p);
                outputList.Add(plotVM);
            }
            return Ok(outputList);
        }

        // Read
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Plot p = await _service.GetPlot(id);
            if (p == null)
                return NotFound("Plot not found");

            PlotVM plotVM = new PlotVM();
            plotVM.Map(p);
            return Ok(plotVM);
        }

        // Create
        [HttpPost]
        public ActionResult Create(PlotVM plotVM)
        {
            Plot plot = plotVM.MapOut();
            _service.CreatePlot(plot, int.Parse(plotVM.HomeownerID ?? ""));
            return Ok();
        }

        // Update
        [HttpPut]
        public ActionResult Update(PlotVM plotVM)
        {
            Plot plot = plotVM.MapOut();
            var rowsImpacted = _service.UpdatePlot(plot, int.Parse(plotVM.HomeownerID ?? "")).Result;
            if (rowsImpacted > 0)
            {
                return Ok();
            }
            return NotFound("Plot not found");
        }

        // Delete
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeletePlot(id);
            return Ok();
        }
    }
}
