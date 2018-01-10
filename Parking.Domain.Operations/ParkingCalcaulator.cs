using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;
using Parking.Interfaces;
using Parking.Interfaces.Application;
using Parking.Interfaces.ApplicationLayer_Interface;
using static Parking.DomainModel.ParkingRates;

namespace Parking.Domain.Services
{   
    public class ParkingRatesCalculator : IParkingRatesCalculator
    {
  
        private readonly IFlatRates _flatRates;
        private readonly IHourlyRates _hourlyRates;

        public ParkingRatesCalculator(IFlatRates FlatRates , IHourlyRates HourlyRates)
        {            
            this._flatRates = FlatRates;
            this._hourlyRates = HourlyRates;
        }
     
        //Parking Calculations - The method checks for parking rates both in Flat and Hourly data list
        //and returns where in the given duration Falls
        public async Task<ParkingRates> Calculations(DateTime Start, DateTime End)
        {
            //Check if patron qualifies for FlatRates
            var parkingRate = await _flatRates.Calculate(Start, End);

            //If patron doesn't qualify for FlatRates , calculate on hourly rate 
            if (parkingRate.Price == 0)                
                parkingRate = await _hourlyRates.Calculate(Start, End);

            return parkingRate;
        }
        
     

      

    }
}
