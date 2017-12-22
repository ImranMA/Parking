using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parking.DomainModel;

namespace Parking.Web.Models.Properties
{
    public class ParkingRatesResourceModel
    {
        public RateCategory Name { get; set; }        
        public double Price { get; set; }
        
        public enum RateCategory
        {
            STANDARD = 0,
            EARLY_BIRD = 1,
            NIGHT = 2,
            WEEKEND = 3
        }

       
    }
}
