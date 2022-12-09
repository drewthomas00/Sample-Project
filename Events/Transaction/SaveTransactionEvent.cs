using System.Transactions;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Transaction
{
    public class SaveTransactionEvent : IUiBusEvent
    {
        private readonly ITransactionService _service;

        public TransactionVM _transaction = new TransactionVM();

        public SaveTransactionEvent(ITransactionService service, TransactionVM transaction)
        {
            _service = service;
            _transaction.Copy(transaction);
            Task.Run(() => SaveTransaction()).GetAwaiter().GetResult();
        }
        private async Task SaveTransaction()
        {
            if (_transaction.TransactionID == null || _transaction.TransactionID == "")
            {
                Model.Transaction transaction = _transaction.MapOut();
                await _service.CreateTransaction(transaction, int.Parse(_transaction.FeeID));
            }
            else
            {
                Model.Transaction transaction = _transaction.MapOut();
                await _service.UpdateTransaction(transaction, int.Parse(_transaction.FeeID));
            }
        }
    }
}
