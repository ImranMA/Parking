using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;

namespace Parking.Interfaces.ApplicationLayer_Interface
{
    public interface IFlatRates
    {
        Task<ParkingRates> Calculate(DateTime start, DateTime end);
    }
}
