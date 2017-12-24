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
     
        //Parking Calculations - The method checks for parking rates both in Flat and Hourly data list
        //and returns where in the given duration Falls
        public async Task<ParkingRates> Calculations(DateTime Start, DateTime End)
        {
            //Check if patron qualifies for FlatRates
            var parkingRate = await CalculateFlatRates(Start, End);

            //If patron doesn't qualify for FlatRates , calculate on hourly rate 
            if (parkingRate.Price == 0)                
                parkingRate = await CalculateHourlyRates(Start, End);

            return parkingRate;
        }
        
        //Calculate the FlatRates between given range
        public async Task<ParkingRates> CalculateFlatRates(DateTime start, DateTime end)
        {
            ParkingRates objParkingRates = new ParkingRates();

            var serviceFlatRatesList = await _parkingRatesRepository.GetAllFlatRates();

            //Compare the given duration in FlatRates List - The expression is checking the selected Entry and Exit duration
            //Range in the FlatRates List, it matches the day of the week and time

            var parkingRatesDomainObject = serviceFlatRatesList.AsEnumerable().Where
            (item => CheckFlatRateDuration(item.Entry, start.TimeOfDay) == true && CheckFlatRateDuration(item.Exit, end.TimeOfDay) == true &&
             item.Days.Contains(((WeekDays)start.DayOfWeek)) && item.Days.Contains(((WeekDays)end.DayOfWeek))).
            Select(item => item);
            
            //If the duration falls into FlatRates
            if (parkingRatesDomainObject.Any())
            {
                objParkingRates = parkingRatesDomainObject.FirstOrDefault(); 
                
                //Pick the Name of FlatRates category
                objParkingRates.Name = Enum.GetName(typeof(RateCategory), objParkingRates.Category);
            }

            return objParkingRates;
        }

        //Calculate the Hourly Rates between given range
        public async Task<ParkingRates> CalculateHourlyRates(DateTime start, DateTime end)
        {
            ParkingRates objParkingRates = new ParkingRates();

            //The fraction of next hour converted to full hour e.g. 1.3 hours mean 2 hours
            double hoursStayed = Math.Ceiling((double)(end - start).TotalMinutes / 60);

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


        //Check if selected duration falls into the Flat Rates time duration range (Entry and Exit )       
        //e.g. If Entry time is selected 07:00AM the method will return true as given GivenFlatRatesRange parameter 
        //for Early Bird is between 06:00 - 09:00
        public bool CheckFlatRateDuration(DurationFlatRates GivenFlatRatesRange, TimeSpan SelectedTime)
        {
            TimeSpan start = GivenFlatRatesRange.Start;
            TimeSpan end = GivenFlatRatesRange.End;
            TimeSpan selected = SelectedTime;

            if (start <= end)
            {
                // start and stop times are in the same day
                if (selected >= start && selected <= end)
                {
                    return true;
                }
            }
            else
            {
                // start and stop times are in different days
                if (selected >= start || selected <= end)
                {
                    return true;
                }
            }

            return false;
        }



    }
}
