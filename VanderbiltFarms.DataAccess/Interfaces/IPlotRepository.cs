using VanderbiltFarms.Model;

namespace VanderbiltFarms.DataAccess.Interfaces
{
    public interface IPlotRepository
    {
        /// <summary>
        /// Create a Plot Record
        /// </summary>
        public Task CreatePlot(Plot plot, int? homeownerId);

        /// <summary>
        /// All Plots
        /// </summary>
        public Task<List<Plot>> GetPlots();

        /// <summary>
        /// Get Plot by Id
        /// </summary>
        public Task<Plot> GetPlot(int plotId);

        /// <summary>
        /// Get Plots by HomeownerId
        /// </summary>
        public Task<List<Fee>> GetPlotsForHomeowner(int homeownerId);

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