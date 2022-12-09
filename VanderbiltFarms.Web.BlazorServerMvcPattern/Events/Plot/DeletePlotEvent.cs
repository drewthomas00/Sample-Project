using Microsoft.AspNetCore.Components;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Pages;


namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Plot
{
    public class DeletePlotEvent : IUiBusEvent
    {
        private readonly IPlotService _service;

        public int _plotid;

        public DeletePlotEvent(IPlotService service, string plotid)
        {
            _service = service;
            _plotid = int.Parse(plotid);
            Task.Run(() => DeletePlot()).GetAwaiter().GetResult();
        }
        private async Task DeletePlot()
        {
            await _service.DeletePlot(_plotid);
        }
    }
}
