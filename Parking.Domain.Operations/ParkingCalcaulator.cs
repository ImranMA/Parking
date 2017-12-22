using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;
using Parking.Interfaces;
using Parking.Interfaces.Application;
using static Parking.DomainModel.ParkingRates;

namespace Parking.Domain.Operations
{   
    public class ParkingRatesCalculator : IParkingRatesCalculator
    {
        private readonly IParkingRates _parkingRatesRepository;

        public ParkingRatesCalculator(IParkingRates parkingRatesRepository)
        {
            this._parkingRatesRepository = parkingRatesRepository;            
        }


        public async Task<ParkingRates> Calculations(DateTime Start, DateTime End)
        {
            //Check if Qualifies for the FlatRates
            var parkingRate = await HasFlatRate(Start, End);

            //If patron doesn't qualify for FlatRate
            if (parkingRate.Price == 0)
                //Else Calulate based on the normal rates
                parkingRate = await HasNormalRate(Start, End);

            return parkingRate;
        }


        public async Task<ParkingRates> HasFlatRate(DateTime start, DateTime end)
        {
            var serviceFlatRatesList = await _parkingRatesRepository.GetAllFlatRates();

            var parkingRatesDomainObject = serviceFlatRatesList.AsEnumerable().Where
            (item => DoesFallIntoTheTimeRang(item.Entry, start.TimeOfDay) == true && DoesFallIntoTheTimeRang(item.Exit, end.TimeOfDay) == true &&
             item.Days.Contains(((WeekDays)start.DayOfWeek)) && item.Days.Contains(((WeekDays)end.DayOfWeek))).
            Select(item => item);


            ParkingRates parkingResourceModel = new ParkingRates();


            if (parkingRatesDomainObject.Any())
            {
                parkingResourceModel = parkingRatesDomainObject.FirstOrDefault();
            }

            return parkingResourceModel;
        }

        public async Task<ParkingRates> HasNormalRate(DateTime start, DateTime end)
        {
            double hoursStayed = Math.Ceiling((double)(end - start).TotalMinutes / 60);
            var serviceHourlyRatesList = await _parkingRatesRepository.GetAllHourlyRates();

            var parkingDTO = serviceHourlyRatesList.AsEnumerable().Where
            (item => (hoursStayed > item.Hours.minHoursStayed) && (hoursStayed <= item.Hours.maxHoursStayed)).
            Select(item => item);

            ParkingRates parkingResourceModel = new ParkingRates();

            if (parkingDTO.Any())
            {
                parkingResourceModel = parkingDTO.FirstOrDefault();
            }

            return parkingResourceModel;
        }

        public bool DoesFallIntoTheTimeRang(StaySpanFlatRate StayLengh, TimeSpan TimeToCompareInRange)
        {
            TimeSpan start = StayLengh.Start;
            TimeSpan end = StayLengh.End;
            TimeSpan now = TimeToCompareInRange;

            if (start <= end)
            {
                // start and stop times are in the same day
                if (now >= start && now <= end)
                {
                    return true;
                }
            }
            else
            {
                // start and stop times are in different days
                if (now >= start || now <= end)
                {
                    return true;
                }
            }

            return false;
        }



    }
}
