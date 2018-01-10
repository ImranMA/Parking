using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;
using Parking.Interfaces;
using Parking.Interfaces.ApplicationLayer_Interface;
using static Parking.DomainModel.ParkingRates;

namespace Parking.Domain.Services
{
    public class HourlyRates : IHourlyRates
    {
        private readonly IParkingRates _parkingRatesRepository;


        public HourlyRates(IParkingRates ParkingRatesRepository)
        {
            this._parkingRatesRepository = ParkingRatesRepository;
        }



        //Calculate the Hourly Rates between given range
        public async Task<ParkingRates> Calculate(DateTime Start, DateTime End)
        {
            ParkingRates objParkingRates = new ParkingRates();

            //The fraction of next hour converted to full hour e.g. 1.3 hours mean 2 hours
            double hoursStayed = Math.Ceiling((double)(End - Start).TotalMinutes / 60);

            //Get the Hourly Rates List
            var serviceHourlyRatesList = await _parkingRatesRepository.GetAllHourlyRates();

            //Compare the given duration in FlatRates List
            var parkingRatesDomainObject = serviceHourlyRatesList.AsEnumerable().Where
            (item => (hoursStayed > item.Hours.MinHours) && (hoursStayed <= item.Hours.MaxHours)).
            Select(item => item);

            //If duration falls into hourly rate range
            if (parkingRatesDomainObject.Any())
            {
                objParkingRates = parkingRatesDomainObject.FirstOrDefault();

                //Pick the Rate Name
                objParkingRates.Name = Enum.GetName(typeof(RateCategory), objParkingRates.Category);

                //If the stay is more than 24 hours then multiply rest of the days with daily rate, 
                if (hoursStayed > 24)
                    objParkingRates.Price = Math.Ceiling((double)hoursStayed / 24) * objParkingRates.Price;
            }

            return objParkingRates;
        }


    }
}
