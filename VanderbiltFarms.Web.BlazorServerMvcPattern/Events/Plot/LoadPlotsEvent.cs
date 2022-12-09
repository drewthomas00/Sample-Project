using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Shared.ViewModel;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Plot
{
    public class LoadPlotsEvent : IUiBusEvent
    {
        private readonly IPlotService _service;
        public List<PlotVM> _plots = new List<PlotVM>();

        public LoadPlotsEvent(IPlotService service)
        {
            _service = service;
            _plots = Task.Run(() => GetPlots()).GetAwaiter().GetResult();
        }

        private async Task<List<PlotVM>> GetPlots()
        {
            List<PlotVM> outputList = new List<PlotVM>();
            List<Model.Plot> temp = await _service.GetPlots();
            foreach (Model.Plot p in temp)
            {
                PlotVM plotVM = new PlotVM();
                plotVM.Map(p);
                outputList.Add(plotVM);
            }
            return outputList;
        }
    }
}

