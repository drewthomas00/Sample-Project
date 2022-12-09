using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;


namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Fee
{
    public class DeleteFeeEvent : IUiBusEvent
    {
        private readonly IFeeService _service;

        public int _feeid;

        public DeleteFeeEvent(IFeeService service, string feeid)
        {
            _service = service;
            _feeid = int.Parse(feeid);
            Task.Run(() => DeleteFee()).GetAwaiter().GetResult();
        }
        private async Task DeleteFee()
        {
            await _service.DeleteFee(_feeid);
        }
    }
}
