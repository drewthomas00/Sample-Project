using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Homeowner;
using VanderbiltFarms.Shared.ViewModel;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    public class NewEditHomeownerController : ControllerComponentBase<NewEditHomeownerView>, IListener<DisplayNewEditHomeownerEvent>
    {
        [Inject]
        protected IHomeownerService _service { get; set; }
        public HomeownerVM? Homeowner { get; set; }


        protected override void OnViewInitialized()
        {
            View.homeowner = Homeowner ?? new HomeownerVM();
            View.HandleValidSubmit = HandleValidSubmit;
            View.Cancel = Cancel;
        }

        public void HandleValidSubmit()
        {
            Bus.Notify(new SaveHomeownerEvent(_service, View.homeowner));
            Bus.Notify(new DisplayNewEditHomeownerEvent(false, new HomeownerVM()));
            Bus.Notify(new LoadHomeownersEvent(_service));
        }

        public void Cancel()
        {
            Bus.Notify(new DisplayNewEditHomeownerEvent(false, new HomeownerVM()));
        }
        public void Handle(DisplayNewEditHomeownerEvent theEvent)
        {
            View.homeowner = theEvent._selectedHomeowner;
            View.Display = theEvent.DisplayNewEditHomeowner;
            StateHasChanged();
        }
    }
}
