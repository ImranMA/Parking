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
    public class FlatRates : IFlatRates
    {

        private readonly IParkingRates _parkingRatesRepository;


        public FlatRates(IParkingRates ParkingRatesRepository)
        {
            this._parkingRatesRepository = ParkingRatesRepository;
        }


        //Calculate the FlatRates between given range
        public async Task<ParkingRates> Calculate(DateTime Start, DateTime End)
        {
            ParkingRates objParkingRates = new ParkingRates();

            var serviceFlatRatesList = await _parkingRatesRepository.GetAllFlatRates();

            //Compare the given duration in FlatRates List - The expression is checking the selected Entry and Exit duration
            //Range in the FlatRates List, it matches the day of the week and time

            var parkingRatesDomainObject = serviceFlatRatesList.AsEnumerable().Where
            (item => CheckFlatRateDuration(item.Entry, Start.TimeOfDay) == true && CheckFlatRateDuration(item.Exit, End.TimeOfDay) == true &&
             item.Days.Contains(Start.DayOfWeek) && item.Days.Contains(End.DayOfWeek)).
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
