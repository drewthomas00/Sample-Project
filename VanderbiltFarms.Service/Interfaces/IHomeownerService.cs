using VanderbiltFarms.Model;

namespace VanderbiltFarms.Service.Interfaces
{
    public interface IHomeownerService
    {
        /// <summary>
        /// Create a Homeowner Record
        /// </summary>
        public Task CreateHomeowner(Homeowner homeowner);

        /// <summary>
        /// All Homeowners
        /// </summary>
        public Task<List<Homeowner>> GetHomeowners();

        /// <summary>
        /// Get Homeowner by Id
        /// </summary>
        public Task<Homeowner> GetHomeowner(int homeownerId);

        /// <summary>
        /// Get Plots by HomeownerId
        /// </summary>
        public Task<List<Fee>> GetPlotsForHomeowner(int homeownerId);

        /// <summary>
        /// Update Homeowner by Id. Returns number of rows impacted.
        /// </summary>
        public Task<int> UpdateHomeowner(Homeowner homeowner);

        /// <summary>
        /// Delete Homeowner by Id
        /// </summary>
        public Task DeleteHomeowner(int homeownerId);

        /// <summary>
        /// Search Homeowner by Name
        /// </summary>
        public Task<List<Homeowner>> SearchHomeowner(string searchTerm);
    }
}