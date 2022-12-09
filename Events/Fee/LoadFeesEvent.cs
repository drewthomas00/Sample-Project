using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Fee
{
    public class LoadFeesEvent : IUiBusEvent
    {
        private readonly IFeeService _service;

        public List<FeeVM> _fees = new List<FeeVM>();

        public LoadFeesEvent(IFeeService service)
        {
            _service = service;
            _fees = Task.Run(() => GetFees()).GetAwaiter().GetResult();
        }

        private async Task<List<FeeVM>> GetFees()
        {
            List<FeeVM> outputList = new List<FeeVM>();
            List<Model.Fee> temp = await _service.GetFees();
            foreach (Model.Fee f in temp)
            {
                FeeVM feeVM = new FeeVM();
                feeVM.Map(f);
                outputList.Add(feeVM);
            }
            return outputList;
        }
    }
}

