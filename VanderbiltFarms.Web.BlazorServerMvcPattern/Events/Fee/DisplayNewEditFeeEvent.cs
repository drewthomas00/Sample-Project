using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Fee
{
    public class DisplayNewEditFeeEvent : IUiBusEvent
    {
        public bool DisplayNewEditFee { get; }
        public FeeVM? _selectedFee = new FeeVM();

        public DisplayNewEditFeeEvent(bool isDisplay, FeeVM f)
        {
            DisplayNewEditFee = isDisplay;
            _selectedFee.Copy(f);
        }
    }
}
