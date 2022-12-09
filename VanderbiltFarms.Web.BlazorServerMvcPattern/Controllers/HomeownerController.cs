using Microsoft.AspNetCore.Components;
using VanderbiltFarms.DataAccess;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Homeowner;
using VanderbiltFarms.Shared.ViewModel;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;
using Microsoft.AspNetCore.Authorization;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    [Route("/homeowner")]
    [Authorize]
    public class HomeownerController : ControllerComponentBase<HomeownerView>,  IListener<LoadHomeownersEvent>, IListener<DeleteHomeownerEvent>
    {
        [Inject]
        protected IHomeownerService _service { get; set; }
        private void NewHomeowner()
        {
            Bus.Notify(new DisplayNewEditHomeownerEvent(true, new HomeownerVM()));
        }

        private void EditHomeowner()
        {
            Bus.Notify(new DisplayNewEditHomeownerEvent(true, View.SelectedHomeowner ?? new HomeownerVM()));
        }

        private void DeleteHomeowner()
        {
            Bus.Notify(new DeleteHomeownerEvent(_service, View.SelectedHomeowner.HomeownerID ?? ""));
        }

        protected override void OnViewInitialized()
        {
            View.SelectedHomeowner = new HomeownerVM();
            View.Model = new List<HomeownerVM>();
            View.DispalyNewEditHomeowner = FragmentBuilder.GetRenderFragment<NewEditHomeownerController>();
            View.OnNewHomeownerClicked = NewHomeowner;
            View.OnEditHomeownerClicked = EditHomeowner;
            View.OnDeleteHomeownerClicked = DeleteHomeowner;
            Bus.Notify(new LoadHomeownersEvent(_service));
        }

        public void Handle(LoadHomeownersEvent theEvent)
        {
            View.Model = new List<HomeownerVM>();
            foreach (HomeownerVM h in theEvent._homeowners)
            {
                HomeownerVM temp = new HomeownerVM();
                temp.Copy(h);
                View.Model.Add(temp);
            }
            StateHasChanged();
        }

        public void Handle(DeleteHomeownerEvent theEvent)
        {
            Bus.Notify(new LoadHomeownersEvent(_service));
        }
    }
}
