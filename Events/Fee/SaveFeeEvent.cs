using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Fee
{
    public class SaveFeeEvent : IUiBusEvent
    {
        private readonly IFeeService _service;

        public FeeVM _fee = new FeeVM();

        public SaveFeeEvent(IFeeService service, FeeVM fee)
        {
            _service = service;
            _fee.Copy(fee);
            Task.Run(() => SaveFee()).GetAwaiter().GetResult();
        }
        private async Task SaveFee()
        {
            if (_fee.FeeID == null || _fee.FeeID == "")
            {
                Model.Fee fee = _fee.MapOut();
                await _service.CreateFee(fee, int.Parse(_fee.PlotID));
            }
            else
            {
                Model.Fee fee = _fee.MapOut();
                await _service.UpdateFee(fee, int.Parse(_fee.PlotID));
            }
        }
    }
}
