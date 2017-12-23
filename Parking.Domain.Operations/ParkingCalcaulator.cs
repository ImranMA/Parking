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
            //Check if patron qualifies for FlatRates
            var parkingRate = await HasFlatRate(Start, End);

            //If patron doesn't qualify for FlatRates , calculate on hourly rate 
            if (parkingRate.Price == 0)                
                parkingRate = await HasNormalRate(Start, End);

            return parkingRate;
        }


        public async Task<ParkingRates> HasFlatRate(DateTime start, DateTime end)
        {
            ParkingRates parkingDomailModel = new ParkingRates();

            var serviceFlatRatesList = await _parkingRatesRepository.GetAllFlatRates();

            var parkingRatesDomainObject = serviceFlatRatesList.AsEnumerable().Where
            (item => DoesFallIntoTheTimeRang(item.Entry, start.TimeOfDay) == true && DoesFallIntoTheTimeRang(item.Exit, end.TimeOfDay) == true &&
             item.Days.Contains(((WeekDays)start.DayOfWeek)) && item.Days.Contains(((WeekDays)end.DayOfWeek))).
            Select(item => item);
            

            if (parkingRatesDomainObject.Any())
            {
                parkingDomailModel = parkingRatesDomainObject.FirstOrDefault();
                parkingDomailModel.Name = Enum.GetName(typeof(RateCategory), parkingDomailModel.Category);
            }

            return parkingDomailModel;
        }

        public async Task<ParkingRates> HasNormalRate(DateTime start, DateTime end)
        {
            ParkingRates parkingDomailModel = new ParkingRates();
            double hoursStayed = Math.Ceiling((double)(end - start).TotalMinutes / 60);
            var serviceHourlyRatesList = await _parkingRatesRepository.GetAllHourlyRates();


            var parkingRatesDomainObject = serviceHourlyRatesList.AsEnumerable().Where
            (item => (hoursStayed > item.Hours.minHoursStayed) && (hoursStayed <= item.Hours.maxHoursStayed)).
            Select(item => item);
                        

            if (parkingRatesDomainObject.Any())
            {
                parkingDomailModel = parkingRatesDomainObject.FirstOrDefault();
                parkingDomailModel.Name = Enum.GetName(typeof(RateCategory), parkingDomailModel.Category);

                //If the stay is more than 24 hours thne multiply rest of the days with daily rate, 
                if (hoursStayed > 24)
                    parkingDomailModel.Price = Math.Ceiling((double)hoursStayed / 24) * parkingDomailModel.Price;
            }

            return parkingDomailModel;
        }
            
        public bool DoesFallIntoTheTimeRang(StayDurationFlatRate StayLengh, TimeSpan TimeToCompareInRange)
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
