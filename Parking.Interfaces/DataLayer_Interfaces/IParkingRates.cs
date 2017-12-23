using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;

namespace Parking.Interfaces
{
    public interface IParkingRates
    {
        //Fetch All Flat Rates from Data store
        Task<IEnumerable<ParkingRates>> GetAllFlatRates();

        //Fetch All Hourly Rates from Data Store
        Task<IEnumerable<ParkingRates>> GetAllHourlyRates();
    }
}
