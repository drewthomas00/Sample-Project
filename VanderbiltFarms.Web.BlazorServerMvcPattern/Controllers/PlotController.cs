using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Plot;
using VanderbiltFarms.Shared.ViewModel;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;
using Microsoft.AspNetCore.Authorization;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers
{
    [Route("/plot")]
    [Authorize]
    public class PlotController : ControllerComponentBase<PlotView>, IListener<LoadPlotsEvent>, IListener<DeletePlotEvent>
    {
        [Inject]
        protected IPlotService _service { get; set; }
        private void NewPlot()
        {
            Bus.Notify(new DisplayNewEditPlotEvent(true, new PlotVM()));
        }

        private void EditPlot()
        {
            Bus.Notify(new DisplayNewEditPlotEvent(true, View.SelectedPlot ?? new PlotVM()));
        }

        private void DeletePlot()
        {
            Bus.Notify(new DeletePlotEvent(_service, View.SelectedPlot.PlotID ?? ""));
        }

        protected override void OnViewInitialized()
        {
            View.SelectedPlot = new PlotVM();
            View.Model = new List<PlotVM>();
            View.DisplayNewEditPlot = FragmentBuilder.GetRenderFragment<NewEditPlotController>();
            View.OnNewPlotClicked = NewPlot;
            View.OnEditPlotClicked = EditPlot;
            View.OnDeletePlotClicked = DeletePlot;
            Bus.Notify(new LoadPlotsEvent(_service));
        }

        public void Handle(LoadPlotsEvent theEvent)
        {
            View.Model = new List<PlotVM>();
            foreach (PlotVM p in theEvent._plots)
            {
                PlotVM temp = new PlotVM();
                temp.Copy(p);
                View.Model.Add(temp);
            }
            StateHasChanged();
        }
        public void Handle(DeletePlotEvent theEvent)
        {
            Bus.Notify(new LoadPlotsEvent(_service));
        }
    }
}
