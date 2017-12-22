using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Parking.DomainModel;
using Parking.Interfaces;
using Parking.Web.Models.Properties;
using static Parking.DomainModel.ParkingRates;

namespace Parking.Web.Models.Behaviour
{
    public class ParkingRatesCalculator
    {
        private readonly IParkingRates _parkingRates;
        private readonly IMapper _mapper;

        public ParkingRatesCalculator(IParkingRates parkingRates, IMapper mapper)
        {
            this._parkingRates = parkingRates;
            this._mapper = mapper;
        }


        public async Task<ParkingRatesResourceModel> CalculateRatesAsync(DateTime start, DateTime end)
        {
            //Check if Qualifies for the FlatRates
            var rest = await hasFlatRate(start,end);

            if (rest.Price == 0)
                //Else Calulate based on the normal rates
                rest = await hasNormalRate(start, end);
            
            return rest;
        }
        

        public async Task<ParkingRatesResourceModel> hasFlatRate(DateTime start, DateTime end)
        {
            var serviceFlatRatesList = await _parkingRates.GetAllFlatRates();

                var parkingDTO = serviceFlatRatesList.AsEnumerable().Where
                (item => findRange(item.Entry, start.TimeOfDay) == true && findRange(item.Exit, end.TimeOfDay) == true &&
                 item.Days.Contains(((WeekDays)start.DayOfWeek)) && item.Days.Contains(((WeekDays)end.DayOfWeek))).
                Select(item => item);


            ParkingRatesResourceModel parkingResourceModel = new ParkingRatesResourceModel();


            if (parkingDTO.Any())
            {
                parkingResourceModel  = _mapper.Map<ParkingRates, ParkingRatesResourceModel>(parkingDTO.FirstOrDefault());
            }            

            return parkingResourceModel;
        }

        public async Task<ParkingRatesResourceModel> hasNormalRate(DateTime start, DateTime end)
        {
            double hoursStayed = Math.Ceiling((double)(end - start).TotalMinutes / 60);
            var serviceHourlyRatesList = await _parkingRates.GetAllHourlyRates();

            var parkingDTO = serviceHourlyRatesList.AsEnumerable().Where
            (item => (hoursStayed > item.Hours.minHoursStayed) && (hoursStayed <= item.Hours.maxHoursStayed)).
            Select(item => item);

            ParkingRatesResourceModel parkingResourceModel = new ParkingRatesResourceModel();
            
            if (parkingDTO.Any())
            {
                parkingResourceModel = _mapper.Map<ParkingRates, ParkingRatesResourceModel>(parkingDTO.FirstOrDefault());
            }

            return parkingResourceModel;
        }

        public bool findRange(StaySpanFlatRate duration, TimeSpan timeToCompareInRange)
        {
            TimeSpan start = duration.Start; 
            TimeSpan end = duration.End; 
            TimeSpan now = timeToCompareInRange;

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
