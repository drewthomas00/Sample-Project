﻿using VanderbiltFarms.Model;

namespace VanderbiltFarms.Service.Interfaces
{
    public interface IFeeService
    {
        /// <summary>
        /// Create a Fee Record
        /// </summary>
        public Task CreateFee(Fee fee, int plotId);

        /// <summary>
        /// All Fees
        /// </summary>
        public Task<List<Fee>> GetFees();

        /// <summary>
        /// Get Fee by Id
        /// </summary>
        public Task<Fee> GetFee(int feeId);

        /// <summary>
        /// Update Homeowner by Id. Returns number of rows impacted.
        /// </summary>
        public Task<int> UpdateFee(Fee fee, int plotId);

        /// <summary>
        /// Delete Homeowner by Id
        /// </summary>
        public Task DeleteFee(int feeId);
    }
}