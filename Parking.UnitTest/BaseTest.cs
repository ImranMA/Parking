using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Parking.DomainModel;
using Parking.Web.Models.Properties;

namespace Parking.UnitTest
{
    public class TestDomainObject
    {
        public DateTime Entry { get; set; }
        public DateTime Exit { get; set; }
        public ParkingRates Expected { get; set; }        

    }
    public class BaseTest
    {

        public readonly IMapper _Mapper;        

        //Base constructore to initialzie the dependencies
        public BaseTest()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ParkingRates, ParkingRatesResourceModel>();
            });

            _Mapper = config.CreateMapper();
            
        }
    }
}
