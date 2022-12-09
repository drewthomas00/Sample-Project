using VanderbiltFarms.Model;

namespace VanderbiltFarms.Service.Interfaces
{
    public interface IPlotService
    {
        /// <summary>
        /// Create a Plot Record
        /// </summary>
        public Task CreatePlot(Plot plot, int? homeownerId);

        /// <summary>
        /// Get Plot by Id
        /// </summary>
        public Task<Plot> GetPlot(int plotId);

        /// <summary>
        /// Get Fees by PlotId
        /// </summary>
        public Task<List<Fee>> GetFeesForPlot(int plotId);

        /// <summary>
        /// All Plots
        /// </summary>
        public Task<List<Plot>> GetPlots();

        /// <summary>
        /// Update Plot by Id. Returns number of rows impacted.
        /// </summary>
        public Task<int> UpdatePlot(Plot plot, int? homeownerId);

        /// <summary>
        /// Delete Plot by Id
        /// </summary>
        public Task DeletePlot(int plotId);
    }
}