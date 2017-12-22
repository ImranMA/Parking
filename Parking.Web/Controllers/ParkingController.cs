using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.DomainModel;
using Parking.Interfaces;
using Parking.Interfaces.Application;
using Parking.Web.Models.Behaviour;
using Parking.Web.Models.Properties;

namespace Parking.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private readonly IParkingRatesCalculator _parkingRatesCalculator;
        private readonly IMapper _mapper;

        private ParkingController(IParkingRatesCalculator parkingRatesCalculator, IMapper mapper)
        {
            this._parkingRatesCalculator = parkingRatesCalculator;
            this._mapper = mapper;
        }
        

        [HttpPost]
        public async Task<IActionResult> Post(string Date)
        {
            var serviceResult = await _parkingRatesCalculator.Calculations(DateTime.Now, DateTime.Now.AddHours(5));
            var mappedResource = _mapper.Map<ParkingRates,ParkingRatesResourceModel>(serviceResult);
            return Ok(mappedResource);
            
        }

    }
}