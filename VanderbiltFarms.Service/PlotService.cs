using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;

namespace VanderbiltFarms.Service
{
    public class PlotService : IPlotService
    {
        private readonly IFeeRepository _feeRepository;
        private readonly IPlotRepository _plotRepository;

        public PlotService(IPlotRepository plotRepository, IFeeRepository feeRepository)
        {
            _plotRepository = plotRepository;
            _feeRepository = feeRepository;
        }

        public async Task CreatePlot(Plot plot, int? homeownerId)
        {
            await _plotRepository.CreatePlot(plot, homeownerId);
        }

        public async Task DeletePlot(int plotId)
        {
            await _plotRepository.DeletePlot(plotId);
        }

        public async Task<List<Fee>> GetFeesForPlot(int plotId)
        {
            return await _feeRepository.GetFeesForPlot(plotId);
        }

        public async Task<Plot> GetPlot(int plotId)
        {
            return await _plotRepository.GetPlot(plotId);
        }
        public async Task<List<Plot>> GetPlots()
        {
            return (await _plotRepository.GetPlots())
                .OrderBy(x => x.PlotId).ToList();
        }

        public async Task<int> UpdatePlot(Plot plot, int? homeownerId)
        {
            return await _plotRepository.UpdatePlot(plot, homeownerId);
        }
    }
}