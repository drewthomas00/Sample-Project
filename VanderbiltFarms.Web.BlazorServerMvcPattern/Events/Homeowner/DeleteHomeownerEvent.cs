using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

public class DeleteHomeownerEvent : IUiBusEvent
{
    private readonly IHomeownerService _service;

    public int _homeownerid;

    public DeleteHomeownerEvent(IHomeownerService service, string homeownerid)
    {
        _service = service;
        _homeownerid = int.Parse(homeownerid);
        Task.Run(() => DeleteHomeowner()).GetAwaiter().GetResult();
    }
    private async Task DeleteHomeowner()
    {
        await _service.DeleteHomeowner(_homeownerid);
    }
}