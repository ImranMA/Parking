using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;

namespace Parking.Interfaces
{
    public interface IParkingRatesData
    {

        //Data List contains all the Flat Parking Rates
        Task<List<ParkingRates>> FlatRatesData();

        //Data List contains all the Hourly Parking Rates
        Task<List<ParkingRates>> HourlyRatesData();
    }
}
