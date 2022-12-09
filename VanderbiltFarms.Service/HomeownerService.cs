using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;

namespace VanderbiltFarms.Service
{
    public class HomeownerService : IHomeownerService
    {
        private readonly IHomeownerRepository _homeownerRepository;
        private readonly IPlotRepository _plotRepository;

        public HomeownerService(IHomeownerRepository homeownerRepository, IPlotRepository plotRepository)
        {
            _plotRepository = plotRepository;
            _homeownerRepository = homeownerRepository;
        }

        public async Task CreateHomeowner(Homeowner homeowner)
        {
            await _homeownerRepository.CreateHomeowner(homeowner);
        }

        public async Task DeleteHomeowner(int homeownerId)
        {
            await _homeownerRepository.DeleteHomeowner(homeownerId);
        }

        public async Task<Homeowner> GetHomeowner(int homeownerId)
        {
            return await _homeownerRepository.GetHomeowner(homeownerId);
        }

        public async Task<List<Homeowner>> GetHomeowners()
        {
            return (await _homeownerRepository.GetHomeowners())
                            .OrderBy(x => x.HomeownerId).ToList();
        }

        public async Task<List<Fee>> GetPlotsForHomeowner(int homeownerId)
        {
            return await _plotRepository.GetPlotsForHomeowner(homeownerId);
        }

        public async Task<List<Homeowner>> SearchHomeowner(string searchTerm)
        {
            return await _homeownerRepository.SearchHomeowner(searchTerm);
        }

        public async Task<int> UpdateHomeowner(Homeowner homeowner)
        {
            return await _homeownerRepository.UpdateHomeowner(homeowner);
        }
    }
}