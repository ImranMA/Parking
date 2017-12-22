using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.DomainModel
{
    public class StaySpanHourlyRate
    {
        public int minHoursStayed { get; set; }
        public int maxHoursStayed { get; set; }
    }
}
