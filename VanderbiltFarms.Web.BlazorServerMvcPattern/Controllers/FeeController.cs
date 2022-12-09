using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Fee;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Transaction;
using VanderbiltFarms.Shared.ViewModel;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;
using Microsoft.AspNetCore.Authorization;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    [Route("/fee")]
    [Authorize]
    public class FeeController : ControllerComponentBase<FeeView>, IListener<LoadFeesEvent>, IListener<LoadTransactionsEvent>, IListener<DeleteFeeEvent>
    {
        [Inject]
        protected IFeeService _service { get; set; }
        [Inject]
        protected ITransactionService _transService { get; set; }

        private void NewFee()
        {
            Bus.Notify(new DisplayNewEditFeeEvent(true, new FeeVM()));
        }

        private void NewTransaction()
        {
            Bus.Notify(new DisplayNewEditTransactionEvent(true, View.SelectedTransaction));
        }

        private void EditFee()
        {
            Bus.Notify(new DisplayNewEditFeeEvent(true, View.SelectedFee ?? new FeeVM()));
        }

        private void EditTransaction()
        {
            Bus.Notify(new DisplayNewEditTransactionEvent(true, View.SelectedTransaction ?? new TransactionVM()));
        }

        private void DeleteFee()
        {
            Bus.Notify(new DeleteFeeEvent(_service, View.SelectedFee.FeeID ?? ""));
        }

        protected override void OnViewInitialized()
        {
            View.SelectedFee = new FeeVM();
            View.SelectedTransaction = new TransactionVM();
            View.Model = new List<FeeVM>();
            View.DispalyNewEditFee = FragmentBuilder.GetRenderFragment<NewEditFeeController>();
            View.DispalyNewEditTransaction = FragmentBuilder.GetRenderFragment<NewEditTransactionController>();
            View.OnNewFeeClicked = NewFee;
            View.OnPayFeeClicked = NewTransaction;
            View.OnEditFeeClicked = EditFee;
            View.OnEditTransactionClicked = EditTransaction;
            View.OnDeleteFeeClicked = DeleteFee;
            Bus.Notify(new LoadFeesEvent(_service));
            Bus.Notify(new LoadTransactionsEvent(_transService));
        }

        public void Handle(LoadFeesEvent theEvent)
        {
            View.Model = new List<FeeVM>();
            foreach (FeeVM f in theEvent._fees)
            {
                View.Model.Add(f);
            }
            StateHasChanged();
        }

        public void Handle(DeleteFeeEvent theEvent)
        {
            Bus.Notify(new LoadFeesEvent(_service));
        }

        public void Handle(LoadTransactionsEvent theEvent)
        {
            View.Transactions = new List<TransactionVM>();
            foreach (TransactionVM t in theEvent._transactions)
            {
                View.Transactions.Add(t);
            }
            StateHasChanged();
        }
    }
}
