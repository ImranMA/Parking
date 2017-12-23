using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;

namespace Parking.Interfaces.Application
{
    public interface IParkingRatesCalculator
    {
        Task<ParkingRates> Calculations(DateTime start, DateTime end);
    }
}
