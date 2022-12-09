using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Shared;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    public class HeartbeatController : MvcComponentBase
    {
        private Timer? _timer;

        protected override Task OnInitializedAsync()
        {
            _timer = new Timer(_ =>
            {
                InvokeAsync(() =>
                {
                    Bus.Notify(new ApplicationHeartbeatEvent(DateTime.Now));

                });
            }, null, 1000, 1000);

            return base.OnInitializedAsync();
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
