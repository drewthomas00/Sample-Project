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
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private ITransactionService _service;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService service)
        {
            _logger = logger;
            _service = service;
        }

        // Read
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var outputList = new List<TransactionVM>();
            List<Transaction> temp = await _service.GetTransactions();
            if (temp == null)
            {
                return NotFound("No transactions found");
            }
            foreach (Transaction t in temp)
            {
                TransactionVM transactionVM = new TransactionVM();
                transactionVM.Map(t);
                outputList.Add(transactionVM);
            }
            return Ok(outputList);
        }

        // Read
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Transaction t = await _service.GetTransaction(id);
            if (t == null)
                return NotFound("Transaction not found");

            TransactionVM transactionVM = new TransactionVM();
            transactionVM.Map(t);
            return Ok(transactionVM);
        }

        // Create
        [HttpPost]
        public ActionResult Create(TransactionVM transactionVM)
        {
            Transaction transaction = transactionVM.MapOut();
            _service.CreateTransaction(transaction, int.Parse(transactionVM.FeeID ?? ""));
            return Ok();
        }

        // Update
        [HttpPut]
        public ActionResult Update(TransactionVM transactionVM)
        {
            Transaction transaction = transactionVM.MapOut();
            
            var rowsImpacted = _service.UpdateTransaction(transaction, int.Parse(transactionVM.FeeID ?? "")).Result;
            if (rowsImpacted > 0)
            {
                return Ok();
            }
            return NotFound("Transaction not found");
        }
    }
}
