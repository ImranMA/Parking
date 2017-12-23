using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.DomainModel
{
    public class ParkingRates
    {
        public string Name { get; set; }
        public RateCategory Category { get; set; }
        public RateType Type { get; set; }
        public StayDurationFlatRate Entry { get; set; }
        public StayDurationFlatRate Exit { get; set; }
        public StayDurationHourlyRate Hours { get; set; }        
        public List<WeekDays> Days { get; set; }
        public double Price { get; set; }
        
        
        public enum RateCategory
        {
            STANDARD, 
            EARLY_BIRD ,
            NIGHT ,
            WEEKEND            
        }

        public enum RateType
        {
            FLAT,
            HOURLY
        }

        public enum WeekDays
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }
    }
}
