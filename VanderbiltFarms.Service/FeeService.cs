using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Model;
using VanderbiltFarms.Service.Interfaces;

namespace VanderbiltFarms.Service
{
    public class FeeService : IFeeService
    {
        private readonly IFeeRepository _feeRepository;

        public FeeService(IFeeRepository feeRepository)
        {
            _feeRepository = feeRepository;
        }

        public async Task CreateFee(Fee fee, int plotId)
        {
            await _feeRepository.CreateFee(fee, plotId);
        }

        public async Task DeleteFee(int feeId)
        {
            await _feeRepository.DeleteFee(feeId);
        }

        public async Task<Fee> GetFee(int feeId)
        {
            return await _feeRepository.GetFee(feeId);
        }

        public async Task<List<Fee>> GetFees()
        {
            return (await _feeRepository.GetFees())
                        .OrderBy(x => x.FeeId).ToList();
        }

        public async Task<int> UpdateFee(Fee fee, int plotId)
        {
            return await _feeRepository.UpdateFee(fee, plotId);
        }
    }
}