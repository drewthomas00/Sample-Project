using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Fee;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Transaction;
using VanderbiltFarms.Shared.ViewModel;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    public class NewEditTransactionController : ControllerComponentBase<NewEditTransactionView>, IListener<DisplayNewEditTransactionEvent>
    {
        [Inject]
        protected ITransactionService _service { get; set; }
        public TransactionVM? Transaction { get; set; }

        protected override void OnViewInitialized()
        {
            View.transaction = Transaction ?? new TransactionVM();
            View.HandleValidSubmit = HandleValidSubmit;
            View.Cancel = Cancel;
        }

        public void HandleValidSubmit()
        {
            Bus.Notify(new SaveTransactionEvent(_service, View.transaction));
            Bus.Notify(new DisplayNewEditTransactionEvent(false, new TransactionVM()));
            Bus.Notify(new LoadTransactionsEvent(_service));
        }

        public void Cancel()
        {
            Bus.Notify(new DisplayNewEditTransactionEvent(false, new TransactionVM()));
        }

        public void Handle(DisplayNewEditTransactionEvent theEvent)
        {
            View.transaction = theEvent._selectedTransaction;
            View.Display = theEvent.DisplayNewEditTransaction;
            StateHasChanged();
        }
    }
}
