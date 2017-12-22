using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Parking.DomainModel;
using Parking.Web.Models.Properties;

namespace Web
{


    public class MappingProfile : Profile
    {
        //AutoMapper mapping profile registration, called from startup.cs
        public MappingProfile()
        {            
            //DomainModel to ResourceModel Mapping
            CreateMap<ParkingRates, ParkingRatesResourceModel>();       
        }

    }
}
