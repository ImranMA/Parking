using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;
using Parking.Interfaces;
using static Parking.DomainModel.ParkingRates;


namespace Parking.Repository.DataStore
{
    public static class RatesData    {

        public static Task<List<ParkingRates>> FlatRatesData()
        {
            return Task.Run(() => new List<ParkingRates>
            {
                new ParkingRates()
                {
                    Category = RateCategory.EARLY_BIRD,
                    Price = 13,
                    Entry = new StayDurationFlatRate { Start = new TimeSpan(6, 0, 0), End = new TimeSpan(9, 0, 0) },
                    Exit =  new StayDurationFlatRate{ Start = new TimeSpan(16, 0, 0), End = new TimeSpan(23, 30, 0) },
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },

                new ParkingRates()
                {
                    Category = RateCategory.NIGHT ,
                    Price = 6.5,
                    Entry = new StayDurationFlatRate { Start = new TimeSpan(18, 0, 0), End = new TimeSpan(0, 0, 0) },
                    Exit = new StayDurationFlatRate { Start = new TimeSpan(0, 0, 0), End = new TimeSpan(6, 0, 0) },
                    Days = new List<WeekDays>()
                    { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday, WeekDays.Saturday}
                },

                new ParkingRates()
                {
                    Category = RateCategory.WEEKEND,
                    Price = 10,
                    Entry = new StayDurationFlatRate { Start = new TimeSpan(0, 0, 0), End = new TimeSpan(23, 59, 59) },
                    Exit = new StayDurationFlatRate { Start = new TimeSpan(0, 0, 0), End = new TimeSpan(23, 59, 59) },
                    Days = new List<WeekDays>()
                    { WeekDays.Sunday,WeekDays.Saturday}
                }
            });
        }

        public static Task<List<ParkingRates>> HourlyRatesData()
        {
            return Task.Run(() => new List<ParkingRates>
            {
                new ParkingRates()
                {
                    Category = RateCategory.STANDARD,
                    Price = 5,
                    Hours = new StayDurationHourlyRate(){minHoursStayed =0 , maxHoursStayed =1},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
                new ParkingRates()
                {
                    Category = RateCategory.STANDARD,
                    Price = 10,
                    Hours = new StayDurationHourlyRate(){minHoursStayed =1 , maxHoursStayed =2},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
                new ParkingRates()
                {
                    Category = RateCategory.STANDARD,
                    Price = 15,
                    Hours = new StayDurationHourlyRate(){minHoursStayed =2 , maxHoursStayed =3},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
                new ParkingRates()
                {
                    Category = RateCategory.STANDARD,
                    Price = 20,
                    Hours = new StayDurationHourlyRate(){minHoursStayed =3 , maxHoursStayed =96},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
            });
        }
    }
}
