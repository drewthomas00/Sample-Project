using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Plot
{
    public class SavePlotEvent : IUiBusEvent
    {
        private readonly IPlotService _service;

        public PlotVM _plot = new PlotVM();

        public SavePlotEvent(IPlotService service, PlotVM plot)
        {
            _service = service;
            _plot.Copy(plot);
            Task.Run(() => SavePlot()).GetAwaiter().GetResult();
        }
        private async Task SavePlot()
        {
            if (_plot.PlotID == null || _plot.PlotID == "")
            {
                Model.Plot plot = _plot.MapOut();
                await _service.CreatePlot(plot, int.Parse(_plot.HomeownerID ?? ""));
            }
            else
            {
                Model.Plot plot = _plot.MapOut();
                await _service.UpdatePlot(plot, int.Parse(_plot.HomeownerID ?? ""));
            }
        }
    }
}
