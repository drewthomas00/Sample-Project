using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    [Route("/event")]
    public class EventController : ControllerComponentBase<EventView>
    {
        private int _currentCount = 0;

        private void IncrementCount()
        {
            _currentCount++;
            View.Model = _currentCount;
        }
        private void DecrementCount()
        {
            _currentCount--;
            View.Model = _currentCount;
        }
        protected override void OnViewInitialized()
        {
            View.OnIncrement = IncrementCount;
            View.OnDecrement = DecrementCount;
            View.Model = _currentCount;
        }
    }
}
