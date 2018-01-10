using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.DomainModel
{
   
    public class ParkingRates
    {
        public string Name { get; set; }
        public RateCategory Category { get; set; }      
        public DurationFlatRates Entry { get; set; }
        public DurationFlatRates Exit { get; set; }
        public DurationHourlyRates Hours { get; set; }        
        public List<DayOfWeek> Days { get; set; }
        public double Price { get; set; }
        
        
        public enum RateCategory
        {
            STANDARD, 
            EARLY_BIRD ,
            NIGHT ,
            WEEKEND            
        }    
    }
}
