using Microsoft.AspNetCore.Mvc;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.Mvc.Controllers
{
    public class FeeController : Controller
    {
        private readonly IFeeService _feeService;
        private readonly ITransactionService _transactionService;
        private readonly ILogger<FeeController> _logger;

        public FeeController(ILogger<FeeController> logger, IFeeService feeService, ITransactionService transactionService)
        {
            _logger = logger;
            _feeService = feeService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrUpdate(int? id, string plotId)
        {
            FeeVM viewModel = new();

            if (id.HasValue)
            {
                viewModel.Map(await _feeService.GetFee(id.Value));

                return View(viewModel);
            }

            viewModel.PlotID = plotId;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(FeeVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (string.IsNullOrEmpty(viewModel.FeeID))
            {
                //Create
                await _feeService.CreateFee(viewModel.MapOut(), int.Parse(viewModel!.PlotID));
            }
            else
            {
                //Update
                await _feeService.UpdateFee(viewModel.MapOut(), int.Parse(viewModel!.PlotID));
            }

            return RedirectToAction("FeeDetails", "Plot", new { id = viewModel.PlotID });
        }

        [HttpGet]
        public async Task<IActionResult> Pay(int? plotId)
        {
            ViewBag.PlotId = plotId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Pay(TransactionVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _transactionService.CreateTransaction(viewModel.MapOut(), int.Parse(viewModel!.FeeID));

            Fee fee = await _feeService.GetFee(int.Parse(viewModel!.FeeID));

            return RedirectToAction("FeeDetails", "Plot", new { id = fee.PlotId });
        }
    }
}
