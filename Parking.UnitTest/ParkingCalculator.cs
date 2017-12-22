using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Parking.DomainModel;
using Parking.Interfaces;
using Parking.Interfaces.Application;
using Parking.Repository;
using Parking.Repository.DataStore;
using Parking.Web.Controllers;
using Parking.Web.Models.Behaviour;
using Parking.Web.Models.Properties;
using Xunit;

namespace Parking.UnitTest
{
    public class ParkingCalculator : BaseTest
    {

        private ParkingRatesCalculator parkingCal;
        private IParkingRates _parkingRates;
        private IParkingRatesCalculator _ParkingCal;

        public ParkingCalculator()
        {
            // this._parkingRatesData = new RatesDataList();
            //this._parkingRates = new Repository.ParkingRates(_parkingRatesData) ;
          //  this._ParkingCal = new ParkingRatesCalculator();
        }

        [Fact]
        public async Task GivenSpecialCondition_WhenEnterEarly_ThenReturnEarlyBirdRate()
        {
            //this.parkingCal = new ParkingRatesCalculator(_parkingRates, _Mapper);

            

            List<TestDTO> testCases = new List<TestDTO>()
            {
              new TestDTO()
              {    Entry = new DateTime(2017, 1, 2, 18, 0, 0),
                   Exit = new DateTime(2017, 1, 3, 6, 0, 0),
                   Expected = new DomainModel.ParkingRates { Name =DomainModel.ParkingRates.RateCategory.NIGHT , Price = 6.5  }
              },
              new TestDTO()
              {    Entry =  new DateTime(2017, 1, 3, 0, 0, 0),
                   Exit =   new DateTime(2018, 1, 3, 6, 0, 0),
                   Expected = new DomainModel.ParkingRates { Name =DomainModel.ParkingRates.RateCategory.NIGHT , Price = 6.5  }
              }
            };


            foreach (var c in testCases)
            {                
                var returnResult = await parkingCal.CalculateRatesAsync(c.Entry, c.Exit);
                Assert.Equal(returnResult.Price, c.Expected.Price);
                Assert.Equal(returnResult.Name.ToString(), c.Expected.Name.ToString());
            }         

        }

        [Fact]
        public async Task NormalRates()
        {
            this.parkingCal = new ParkingRatesCalculator(_parkingRates, _Mapper);


            List<TestDTO> testCases = new List<TestDTO>()
            {
              new TestDTO()
              {    Entry = new DateTime(2017, 1, 2, 9, 0, 0),
                   Exit = new DateTime(2017, 1, 2, 10, 0, 0),
                   Expected = new DomainModel.ParkingRates { Name =DomainModel.ParkingRates.RateCategory.STANDARD , Price = 5  }
              } ,

               new TestDTO()
              {    Entry = new DateTime(2017, 1, 2, 9, 0, 0),
                   Exit = new DateTime(2017, 1, 2, 10, 20, 0),
                   Expected = new DomainModel.ParkingRates { Name =DomainModel.ParkingRates.RateCategory.STANDARD , Price = 10  }
              } ,

              new TestDTO()
              {    Entry = new DateTime(2017, 1, 2, 10, 0, 0),
                   Exit = new DateTime(2017, 1, 2, 12, 0, 0),
                   Expected = new DomainModel.ParkingRates { Name =DomainModel.ParkingRates.RateCategory.STANDARD , Price = 10  }
              }
            };


            foreach (var c in testCases)
            {
                var returnResult = await parkingCal.CalculateRatesAsync(c.Entry, c.Exit);
                Assert.Equal(returnResult.Price, c.Expected.Price);
                Assert.Equal(returnResult.Name.ToString(), c.Expected.Name.ToString());
            }

        }
    }
}
