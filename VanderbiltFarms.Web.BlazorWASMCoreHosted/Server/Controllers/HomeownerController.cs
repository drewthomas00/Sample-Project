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
    public class HomeownerController : ControllerBase
    {
        private readonly ILogger<HomeownerController> _logger;
        private IHomeownerService _service;

        public HomeownerController(ILogger<HomeownerController> logger, IHomeownerService service)
        {
            _logger = logger;
            _service = service;
        }

        // Read
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var outputList = new List<HomeownerVM>();
            List<Homeowner> temp = await _service.GetHomeowners();
            if (temp == null)
            {
                return NotFound("No homeowners found");
            }
            foreach (Homeowner h in temp)
            {
                HomeownerVM homeownerVM = new HomeownerVM();
                homeownerVM.Map(h);
                outputList.Add(homeownerVM);
            }
            return Ok(outputList);
        }

        // Read
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Homeowner h = await _service.GetHomeowner(id);
            if (h == null) 
                return NotFound("Homeowner not found");

            HomeownerVM homeownerVM = new HomeownerVM();
            homeownerVM.Map(h);
            return Ok(homeownerVM);
        }

        // Create
        [HttpPost]
        public ActionResult Create(HomeownerVM homeownerVM)
        {
            Homeowner homeowner = homeownerVM.MapOut();
            _service.CreateHomeowner(homeowner);
            return Ok();
        }

        // Update
        [HttpPut]
        public ActionResult Update(HomeownerVM homeownerVM)
        {
            Homeowner homeowner = homeownerVM.MapOut();
            var rowsImpacted = _service.UpdateHomeowner(homeowner).Result;
            if (rowsImpacted > 0)
            {
                return Ok();
            }
            return NotFound("Homeowner not found");
        }

        // Delete
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeleteHomeowner(id);
            return Ok();
        }
    }
}
