using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Plot;
using VanderbiltFarms.Shared.ViewModel;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    public class NewEditPlotController : ControllerComponentBase<NewEditPlotView>, IListener<DisplayNewEditPlotEvent>
    {
        [Inject]
        protected IPlotService _service { get; set; }
        public PlotVM? Plot { get; set; }


        protected override void OnViewInitialized()
        {
            View.plot = Plot ?? new PlotVM();
            View.HandleValidSubmit = HandleValidSubmit;
            View.Cancel = Cancel;
        }

        public void HandleValidSubmit()
        {
            Bus.Notify(new SavePlotEvent(_service, View.plot));
            Bus.Notify(new DisplayNewEditPlotEvent(false, new PlotVM()));
            Bus.Notify(new LoadPlotsEvent(_service));
        }

        public void Cancel()
        {
            Bus.Notify(new DisplayNewEditPlotEvent(false, new PlotVM()));
        }
        public void Handle(DisplayNewEditPlotEvent theEvent)
        {
            View.plot = theEvent._selectedPlot;
            View.Display = theEvent.DisplayNewEditPlot;
            StateHasChanged();
        }
    }
}
