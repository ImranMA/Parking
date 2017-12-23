using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;

namespace Parking.Interfaces
{
    public interface IParkingRates
    {
        //Get All FlatParking Rates
        Task<IEnumerable<ParkingRates>> GetAllFlatRates();

        //Get All Hourly Parking Rates
        Task<IEnumerable<ParkingRates>> GetAllHourlyRates();
    }
}
