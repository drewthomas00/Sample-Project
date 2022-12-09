using VanderbiltFarms.Model;
using VanderbiltFarms.Service;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Transaction
{
    public class LoadTransactionsEvent : IUiBusEvent
    {
        private readonly ITransactionService _service;

        public List<TransactionVM> _transactions;

        public LoadTransactionsEvent(ITransactionService service)
        {
            _transactions = new List<TransactionVM>();
            _service = service;
            _transactions = Task.Run(() => GetTransactions()).GetAwaiter().GetResult();
        }

        private async Task<List<TransactionVM>> GetTransactions()
        {
            List<TransactionVM> outputList = new List<TransactionVM>();
            List<Model.Transaction> temp = await _service.GetTransactions();
            foreach (Model.Transaction t in temp)
            {
                TransactionVM transactionVM = new TransactionVM();
                transactionVM.Map(t);
                outputList.Add(transactionVM);
            }
            return outputList;
        }
    }
}

