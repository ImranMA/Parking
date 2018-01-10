using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;

namespace Parking.Interfaces.Application
{
    public interface IParkingRatesCalculator
    {
        //Method to calculate the parking rates for given duration
        Task<ParkingRates> Calculations(DateTime Start, DateTime End);
    }
}
