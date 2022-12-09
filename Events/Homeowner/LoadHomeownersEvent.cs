using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Homeowner
{
    public class LoadHomeownersEvent : IUiBusEvent
    {
        private readonly IHomeownerService _service;

        public List<HomeownerVM> _homeowners;

        public LoadHomeownersEvent(IHomeownerService service)
        {
            _homeowners = new List<HomeownerVM>();
            _service = service;
            _homeowners = Task.Run(() => GetHomeowners()).GetAwaiter().GetResult();
        }

        private async Task<List<HomeownerVM>> GetHomeowners()
        {
            List<HomeownerVM> outputList = new List<HomeownerVM>();
            List<Model.Homeowner> temp = await _service.GetHomeowners();
            foreach (Model.Homeowner h in temp)
            {
                HomeownerVM homeownerVM = new HomeownerVM();
                homeownerVM.Map(h);
                outputList.Add(homeownerVM);
            }
            return outputList;
        }
    }
}

