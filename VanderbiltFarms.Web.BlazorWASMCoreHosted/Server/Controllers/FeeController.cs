using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VanderbiltFarms.Model;
using VanderbiltFarms.Shared.ViewModel;
using VanderbiltFarms.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace VanderbiltFarms.Web.BlazorWASMCoreHosted.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FeeController : ControllerBase
    {
        private readonly ILogger<FeeController> _logger;
        private IFeeService _service;

        public FeeController(ILogger<FeeController> logger, IFeeService service)
        {
            _logger = logger;
            _service = service;
        }

        // Read
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var outputList = new List<FeeVM>();
            List<Fee> temp = await _service.GetFees();
            if (temp == null)
            {
                return NotFound("No fees found");
            }
            foreach (Fee f in temp)
            {
                FeeVM feeVM = new FeeVM();
                feeVM.Map(f);
                outputList.Add(feeVM);
            }
            return Ok(outputList);
        }

        // Read
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Fee f = await _service.GetFee(id);
            if (f == null)
                return NotFound("Fee not found");

            FeeVM feeVM = new FeeVM();
            feeVM.Map(f);
            return Ok(feeVM);
        }

        // Create
        [HttpPost]
        public ActionResult Create(FeeVM feeVM)
        {
            Fee fee = feeVM.MapOut();
            _service.CreateFee(fee, int.Parse(feeVM.PlotID ?? ""));
            return Ok();
        }

        // Update
        [HttpPut]
        public ActionResult Update(FeeVM feeVM)
        {
            Fee fee = feeVM.MapOut();
            var rowsImpacted = _service.UpdateFee(fee, int.Parse(feeVM.PlotID ?? "")).Result;
            if (rowsImpacted > 0)
            {
                return Ok();
            }
            return NotFound("Fee not found");
        }

        // Delete
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeleteFee(id);
            return Ok();
        }
    }
}
