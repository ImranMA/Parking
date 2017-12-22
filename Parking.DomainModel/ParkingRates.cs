using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.DomainModel
{
    public class ParkingRates
    {
        public RateCategory Name { get; set; }
        public RateType Type { get; set; }
        public StaySpanFlatRate Entry { get; set; }
        public StaySpanFlatRate Exit { get; set; }
        public StaySpanHourlyRate Hours { get; set; }        
        public List<WeekDays> Days { get; set; }
        public double Price { get; set; }
        
        
        public enum RateCategory
        {
            STANDARD = 0,
            EARLY_BIRD = 1,
            NIGHT = 2,
            WEEKEND = 3           
        }

        public enum RateType
        {
            FLAT = 0,
            HOURLY = 1
        }

        public enum WeekDays
        {
            Sunday = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6
        }
    }
}
