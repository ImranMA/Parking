using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Domain.Operations;
using Parking.DomainModel;
using Parking.Interfaces;
using Parking.Interfaces.Application;
using Parking.Repository.DataStore;
using Xunit;

namespace Parking.UnitTest
{
    public class ParkingCalculator : BaseTest
    {        
        private IParkingRatesCalculator _ParkingCalculator;
        private IParkingRates _parkingRates;        
           

        public ParkingCalculator()
        {            
            this._parkingRates = new Parking.Repository.ParkingRatesReporsitory();
            this._ParkingCalculator = new ParkingRatesCalculator(_parkingRates);                 
        }

        [Fact]
        public async Task GIVEN_WEEKEND_CASE_RETURN_FLATRATE()
        {
            List<TESTDomainObject> testCases = new List<TESTDomainObject>()
            {
              new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 23, 9, 0, 0),
                   Exit = new DateTime(2017, 12, 23, 10, 0, 0),
                   Expected = new ParkingRates { Name ="WEEKEND" , Price = 10  }
              }
              ,
               new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 23, 00, 0, 0),
                   Exit = new DateTime(2017, 12, 24, 10, 0, 0),
                   Expected = new ParkingRates { Name ="WEEKEND" , Price = 10  }
              }
            };

            foreach (var c in testCases)
            {
                var returnResult = await _ParkingCalculator.Calculations(c.Entry, c.Exit);
                Assert.Equal(returnResult.Price, c.Expected.Price);
                Assert.Equal(returnResult.Name.ToString(), c.Expected.Name.ToString());
            }
        }
        
        [Fact]
        public async Task GIVEN_EARLYBIRD_CASE_RETURN_FLATRATE()
        {
            List<TESTDomainObject> testCases = new List<TESTDomainObject>()
            {
              new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 9, 0, 0),
                   Exit = new DateTime(2017, 12, 22, 18, 0, 0),
                   Expected = new ParkingRates { Name ="EARLY_BIRD" , Price = 13  }
              },
              new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 9, 0, 0),
                   Exit = new DateTime(2017, 12, 22, 18, 0, 0),
                   Expected = new ParkingRates { Name ="EARLY_BIRD" , Price = 13  }
              }
            };

            foreach (var c in testCases)
            {
                var returnResult = await _ParkingCalculator.Calculations(c.Entry, c.Exit);
                Assert.Equal(returnResult.Price, c.Expected.Price);
                Assert.Equal(returnResult.Name.ToString(), c.Expected.Name.ToString());
            }
        }
        
        [Fact]
        public async Task GIVEN_NIGHT_CASE__RETURN_FLATRATE()
        {
            List<TESTDomainObject> testCases = new List<TESTDomainObject>()
            {
              new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 19, 0, 0),
                   Exit = new DateTime(2017, 12, 23, 05, 0, 0),
                   Expected = new ParkingRates { Name ="NIGHT" , Price = 6.5  }
              },

              //Special case when friday night falls into weekend
              new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 19, 0, 0),
                   Exit = new DateTime(2017, 12, 23, 05, 0, 0),
                   Expected = new ParkingRates { Name ="NIGHT" , Price = 6.5  }
              }              
            };
            foreach (var c in testCases)
            {
                var returnResult = await _ParkingCalculator.Calculations(c.Entry, c.Exit);
                Assert.Equal(returnResult.Price, c.Expected.Price);
                Assert.Equal(returnResult.Name.ToString(), c.Expected.Name.ToString());
            }
        }
        
        [Fact]
        public async Task GIVEN_DURATION_NOTUNDER_FLATRATES_RETURN_STANDARD_RATES()
        {           

            List<TESTDomainObject> testCases = new List<TESTDomainObject>()
            {
              new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 9, 0, 0),
                   Exit = new DateTime(2017, 12, 22, 10, 0, 0),
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 5  }
              } ,

               new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 9, 0, 0),
                   Exit = new DateTime(2017, 12, 22, 10, 20, 0),
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 10  }
              } ,

              new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 10, 0, 0),
                   Exit = new DateTime(2017, 12, 22, 12, 0, 0),
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 10  }
              },

              new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 10, 0, 0),
                   Exit = new DateTime(2017, 12, 22, 14, 0, 0),
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 20  }
              }
              ,
              new TESTDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 10, 0, 0),
                   Exit = new DateTime(2017, 12, 24, 14, 0, 0),
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 60  }
              }
            };
            
            foreach (var c in testCases)
            {
                var returnResult = await _ParkingCalculator.Calculations(c.Entry, c.Exit);
                Assert.Equal(returnResult.Price, c.Expected.Price);
                Assert.Equal(returnResult.Name.ToString(), c.Expected.Name.ToString());
            }

        }
    }
}
