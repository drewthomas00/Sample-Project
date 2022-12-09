using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Plot
{
    public class DisplayNewEditPlotEvent : IUiBusEvent
    {
        public bool DisplayNewEditPlot { get; }
        public PlotVM? _selectedPlot = new PlotVM();

        public DisplayNewEditPlotEvent(bool isDisplay, PlotVM p)
        {
            DisplayNewEditPlot = isDisplay;
            _selectedPlot.Copy(p);
        }
    }
}
