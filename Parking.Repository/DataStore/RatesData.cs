using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;
using Parking.Interfaces;
using static Parking.DomainModel.ParkingRates;


namespace Parking.Repository.DataStore
{
    //The Data store contains all the rates values
    public static class RatesData    {


        //Return all FlatRates
        public static Task<List<ParkingRates>> FlatRatesData()
        {
            return Task.Run(() => new List<ParkingRates>
            {
                new ParkingRates()
                {
                    Category = RateCategory.EARLY_BIRD,
                    Price = 13,
                    
                    //Entry Time Condition 06:00 - 09:00
                    Entry = new DurationFlatRates { Start = new TimeSpan(6, 0, 0), End = new TimeSpan(9, 0, 0) },

                    //Exit Time Condition  15:30 - 23:30
                    Exit =  new DurationFlatRates{ Start = new TimeSpan(15, 30, 0), End = new TimeSpan(23, 30, 0) },
                    
                   //Days Allowed
                    Days = new List<WeekDays>()
                    { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },

                new ParkingRates()
                {
                    Category = RateCategory.NIGHT ,
                    Price = 6.5,
                    
                    //Entry Time Condition 18:00 - 00:00
                    Entry = new DurationFlatRates { Start = new TimeSpan(18, 0, 0), End = new TimeSpan(0, 0, 0) },

                    //Exit Time Condition  00:00 - 06:00
                    Exit = new DurationFlatRates { Start = new TimeSpan(0, 0, 0), End = new TimeSpan(6, 0, 0) },

                    //Days Allowed
                    Days = new List<WeekDays>()
                    { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday, WeekDays.Saturday}
                },

                new ParkingRates()
                {
                    Category = RateCategory.WEEKEND,
                    Price = 10,

                    //Entry Time Condition 00:00 - 23:59 - Full Day Saturday,Sunday
                    Entry = new DurationFlatRates { Start = new TimeSpan(0, 0, 0), End = new TimeSpan(23, 59, 59) },

                    //Entry Time Condition 00:00 - 23:59 - Full Day Saturday,Sunday
                    Exit = new DurationFlatRates { Start = new TimeSpan(0, 0, 0), End = new TimeSpan(23, 59, 59) },

                    //Days Allowed
                    Days = new List<WeekDays>()
                    { WeekDays.Sunday,WeekDays.Saturday}
                }
            });
        }


        //Return all Hourly Rates
        public static Task<List<ParkingRates>> HourlyRatesData()
        {
            return Task.Run(() => new List<ParkingRates>
            {
                new ParkingRates()
                {
                    Category = RateCategory.STANDARD,
                    Price = 5,
                   
                    //Hours 0 - 1
                    Hours = new DurationHourlyRates(){MinHours =0 , MaxHours =1},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
                new ParkingRates()
                {
                    Category = RateCategory.STANDARD,
                    Price = 10,
                    
                    //Hours 1 - 2
                    Hours = new DurationHourlyRates(){MinHours =1 , MaxHours =2},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
                new ParkingRates()
                {
                    Category = RateCategory.STANDARD,
                    Price = 15,
                    
                    //Hours 2 - 3
                    Hours = new DurationHourlyRates(){MinHours =2 , MaxHours =3},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
                new ParkingRates()
                {
                    Category = RateCategory.STANDARD,
                    Price = 20,
                    
                    //Hours 3+ - 4days
                    Hours = new DurationHourlyRates(){MinHours =3 , MaxHours =96},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
            });
        }
    }
}
