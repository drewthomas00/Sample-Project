using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

public class SaveHomeownerEvent : IUiBusEvent
{
    private readonly IHomeownerService _service;

    public HomeownerVM _homeowner = new HomeownerVM();

    public SaveHomeownerEvent(IHomeownerService service, HomeownerVM homeowner)
    {
        _service = service;
        _homeowner.Copy(homeowner);
        Task.Run(() => SaveHomeowner()).GetAwaiter().GetResult();
    }
    private async Task SaveHomeowner()
    {
        if (_homeowner.HomeownerID == null || _homeowner.HomeownerID == "")
        {
            Homeowner homeowner = _homeowner.MapOut();
            await _service.CreateHomeowner(homeowner);
        }
        else
        {
            Homeowner homeowner = _homeowner.MapOut();
            await _service.UpdateHomeowner(homeowner);
        }
    }
}

