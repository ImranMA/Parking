using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;
using Parking.Interfaces;


namespace Parking.Repository
{
    public class ParkingRates : IParkingRates
    {
        private readonly IParkingRatesData _parkingRatesData;

        public ParkingRates(IParkingRatesData parkingRatesData)
        {
            this._parkingRatesData = parkingRatesData;
        }

        public async Task<IEnumerable<DomainModel.ParkingRates>> GetAllFlatRates()
        {
            return await _parkingRatesData.FlatRatesData();
        }

        public async Task<IEnumerable<DomainModel.ParkingRates>> GetAllHourlyRates()
        {
            return await _parkingRatesData.HourlyRatesData();
        }
    }
}
