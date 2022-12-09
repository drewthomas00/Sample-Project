using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Shared;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Shared;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    public class MainLayoutController : LayoutControllerComponentBase<MainLayoutView>, IListener<ApplicationHeartbeatEvent>
    {
        protected override void OnViewInitialized()
        {

        }

        public void Handle(ApplicationHeartbeatEvent theEvent)
        {
            View.Time = theEvent.Time.ToLongTimeString();
            StateHasChanged();
        }
    }
}
