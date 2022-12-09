using Microsoft.AspNetCore.Mvc;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.Mvc.Controllers
{
    public class HomeownerController : Controller
    {
        private readonly IHomeownerService _service;

        public HomeownerController(IHomeownerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            List<Homeowner> homeowners;

            if (!string.IsNullOrEmpty(searchString))
            {
                homeowners = await _service.SearchHomeowner(searchString);
            }
            else
            {
                homeowners = await _service.GetHomeowners();
            }

            List<HomeownerVM> viewModel = new();
            foreach (Homeowner h in homeowners)
            {
                HomeownerVM homeownerVM = new();
                homeownerVM.Map(h);
                viewModel.Add(homeownerVM);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrUpdate(int? id)
        {
            if(id.HasValue)
            {
                HomeownerVM viewModel = new();
                viewModel.Map(await _service.GetHomeowner(id.Value));

                return View(viewModel);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(HomeownerVM viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if(string.IsNullOrEmpty(viewModel.HomeownerID))
            {
                //Create
                await _service.CreateHomeowner(viewModel.MapOut());
            }
            else
            {
                //Update
                await _service.UpdateHomeowner(viewModel.MapOut());
            }

            return RedirectToAction("Search");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteHomeowner(id);

            return RedirectToAction("Search");
        }
    }
}