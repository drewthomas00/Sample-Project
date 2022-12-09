using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Homeowner
{
    public class DisplayNewEditHomeownerEvent : IUiBusEvent
    {
        public bool DisplayNewEditHomeowner { get; }
        public HomeownerVM? _selectedHomeowner = new HomeownerVM();

        public DisplayNewEditHomeownerEvent(bool isDisplay, HomeownerVM h)
        {
            DisplayNewEditHomeowner = isDisplay;
            _selectedHomeowner.Copy(h);
        }
    }
}
