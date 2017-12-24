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


        //Injecting Dependencies
        public ParkingController(IParkingRatesCalculator parkingRatesCalculator, IMapper mapper)
        {
            this._parkingRatesCalculator = parkingRatesCalculator;
            this._mapper = mapper;
        }
        

        [HttpPost]
        public async Task<IActionResult> Post(DateTime Start, DateTime End)
        {
            try
            {
                //Controller call to Domain operations Service
                var serviceResult = await _parkingRatesCalculator.Calculations(Start, End);

                //Domain Model to Resource Mapping
                var mappedResourceObject = _mapper.Map<ParkingRates, ParkingRatesResourceModel>(serviceResult);

                return Ok(mappedResourceObject);
            }
            catch(Exception)
            {
                return NotFound();
            }            
        }        
    }
}