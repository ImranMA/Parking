using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Parking.DomainModel;
using Parking.Interfaces;
using static Parking.DomainModel.ParkingRates;

namespace Parking.Repository.DataStore
{
    public class RatesDataList : IParkingRatesData
    {
        public Task<List<DomainModel.ParkingRates>> FlatRatesData()
        {
            return Task.Run(() =>  new List<DomainModel.ParkingRates>
            {
                new DomainModel.ParkingRates()
                {
                    Name = RateCategory.EARLY_BIRD,
                    Price = 13,
                    Entry = new StaySpanFlatRate { Start = new TimeSpan(6, 0, 0), End = new TimeSpan(9, 0, 0) },
                    Exit =  new StaySpanFlatRate{ Start = new TimeSpan(16, 0, 0), End = new TimeSpan(23, 30, 0) },
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },

                new DomainModel.ParkingRates()
                {
                    Name = RateCategory.NIGHT ,
                    Price = 6.5,
                    Entry = new StaySpanFlatRate { Start = new TimeSpan(18, 0, 0), End = new TimeSpan(0, 0, 0) },
                    Exit = new StaySpanFlatRate { Start = new TimeSpan(0, 0, 0), End = new TimeSpan(6, 0, 0) },
                    Days = new List<WeekDays>()
                    { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday, WeekDays.Saturday}
                },

                new DomainModel.ParkingRates()
                {
                    Name = RateCategory.WEEKEND,
                    Price = 10,
                    Entry = new StaySpanFlatRate { Start = new TimeSpan(0, 0, 0), End = new TimeSpan(23, 59, 59) },
                    Exit = new StaySpanFlatRate { Start = new TimeSpan(0, 0, 0), End = new TimeSpan(23, 59, 59) },
                    Days = new List<WeekDays>()
                    { WeekDays.Sunday,WeekDays.Saturday}
                }
            });
        }

        public Task<List<DomainModel.ParkingRates>> HourlyRatesData()
        {
            return Task.Run(() => new List<DomainModel.ParkingRates>
            {
                new DomainModel.ParkingRates()
                {
                    Name = RateCategory.STANDARD,
                    Price = 5,
                    Hours = new StaySpanHourlyRate(){minHoursStayed =0 , maxHoursStayed =1},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
                new DomainModel.ParkingRates()
                {
                    Name = RateCategory.STANDARD,
                    Price = 10,
                    Hours = new StaySpanHourlyRate(){minHoursStayed =1 , maxHoursStayed =2},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
                new DomainModel.ParkingRates()
                {
                    Name = RateCategory.STANDARD,
                    Price = 15,
                    Hours = new StaySpanHourlyRate(){minHoursStayed =2 , maxHoursStayed =3},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
                new DomainModel.ParkingRates()
                {
                    Name = RateCategory.STANDARD,
                    Price = 20,
                    Hours = new StaySpanHourlyRate(){minHoursStayed =4 , maxHoursStayed =24},
                    Days = new List<WeekDays>()
                   { WeekDays.Monday, WeekDays.Tuesday,WeekDays.Wednesday,WeekDays.Thursday,WeekDays.Friday}
                },
            });
        }
    }
}
