using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Fee;
using VanderbiltFarms.Shared.ViewModel;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    public class NewEditFeeController : ControllerComponentBase<NewEditFeeView>, IListener<DisplayNewEditFeeEvent>
    {
        [Inject]
        protected IFeeService _service { get; set; }
        public FeeVM? Fee { get; set; }

        protected override void OnViewInitialized()
        {
            View.fee = Fee ?? new FeeVM();
            View.HandleValidSubmit = HandleValidSubmit;
            View.Cancel = Cancel;
        }

        public void HandleValidSubmit()
        {
            Bus.Notify(new SaveFeeEvent(_service, View.fee));
            Bus.Notify(new DisplayNewEditFeeEvent(false, new FeeVM()));
            Bus.Notify(new LoadFeesEvent(_service));
        }

        public void Cancel()
        {
            Bus.Notify(new DisplayNewEditFeeEvent(false, new FeeVM()));
        }
        public void Handle(DisplayNewEditFeeEvent theEvent)
        {
            View.fee = theEvent._selectedFee;
            View.Display = theEvent.DisplayNewEditFee;
            StateHasChanged();
        }
    }
}
