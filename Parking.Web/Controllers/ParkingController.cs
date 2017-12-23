using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parking.DomainModel;
using Parking.Interfaces.Application;
using Parking.Web.Models.Properties;
using static Parking.Web.Models.Properties.ParkingRatesResourceModel;

namespace Parking.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private readonly IParkingRatesCalculator _parkingRatesCalculator;
        private readonly IMapper _mapper;

        public ParkingController(IParkingRatesCalculator parkingRatesCalculator, IMapper mapper)
        {
            this._parkingRatesCalculator = parkingRatesCalculator;
            this._mapper = mapper;
        }
        

        [HttpPost]
        public async Task<IActionResult> Post(DateTime Start, DateTime End)
        {
            var serviceResult = await _parkingRatesCalculator.Calculations(Start, End);
            var mappedResource = _mapper.Map<ParkingRates,ParkingRatesResourceModel>(serviceResult);

            return Ok(mappedResource);            
        }        
    }
}