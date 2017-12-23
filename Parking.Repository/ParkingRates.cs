using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;
using Parking.Interfaces;
using Parking.Repository.DataStore;

namespace Parking.Repository
{
    public class ParkingRatesReporsitory : IParkingRates
    {               

        //Get Flate Rates from Data Store
        public async Task<IEnumerable<ParkingRates>> GetAllFlatRates()
        {
            return await RatesData.FlatRatesData();
        }


        //Get Hourly Rates from Data Store
        public async Task<IEnumerable<ParkingRates>> GetAllHourlyRates()
        {
            return await RatesData.HourlyRatesData();
        }
    }
}
