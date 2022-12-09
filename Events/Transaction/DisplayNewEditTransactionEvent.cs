using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Transaction
{
    public class DisplayNewEditTransactionEvent : IUiBusEvent
    {
        public bool DisplayNewEditTransaction { get; }
        public TransactionVM? _selectedTransaction = new TransactionVM();

        public DisplayNewEditTransactionEvent(bool isDisplay, TransactionVM t)
        {
            DisplayNewEditTransaction = isDisplay;
            _selectedTransaction.Copy(t);
        }
    }
}