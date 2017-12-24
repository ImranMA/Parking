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
        public async Task GIVEN_WEEKEND_CASE_RETURN_FLATRATES()
        {
            List<TestDomainObject> testCases = new List<TestDomainObject>()
            {
              new TestDomainObject()
              {    Entry = new DateTime(2017, 12, 23, 9, 0, 0),//23/12/2017 09:00 Sat
                   Exit = new DateTime(2017, 12, 23, 10, 0, 0),//23/12/2017 10:00 Sat
                   Expected = new ParkingRates { Name ="WEEKEND" , Price = 10  }
              }
              ,
               new TestDomainObject()
              {    Entry = new DateTime(2017, 12, 23, 00, 0, 0),//23/12/2017 00:00 Sat
                   Exit = new DateTime(2017, 12, 24, 10, 0, 0),//24/12/2017 10:00 Sunday
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
        public async Task GIVEN_EARLYBIRD_CASE_RETURN_FLATRATES()
        {
            List<TestDomainObject> testCases = new List<TestDomainObject>()
            {
              new TestDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 9, 0, 0),// 22/12/2017 09:00 Friday
                   Exit = new DateTime(2017, 12, 22, 18, 0, 0),// 22/12/2017 18:00 Friday
                   Expected = new ParkingRates { Name ="EARLY_BIRD" , Price = 13  }
              },
              //Inclusive Range Test
              new TestDomainObject()
              { 
                   Entry = new DateTime(2017, 12, 22, 6, 0, 0), // 22/12/2017 06:00 Friday 
                   Exit = new DateTime(2017, 12, 22, 23, 30, 0), // 22/12/2017 11:30 Friday
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
        public async Task GIVEN_NIGHT_CASE_RETURN_FLATRATES()
        {
            List<TestDomainObject> testCases = new List<TestDomainObject>()
            {
              new TestDomainObject()
              {    Entry = new DateTime(2017, 12, 21, 19, 0, 0),// 21/12/2017 19:00 Thursday
                   Exit = new DateTime(2017, 12, 22, 05, 0, 0), // 22/12/2017 05:00 Friday
                   Expected = new ParkingRates { Name ="NIGHT" , Price = 6.5  }
              },

              //Special case when friday night falls into weekend
              new TestDomainObject()
              {    Entry = new DateTime(2017, 12, 22, 20, 0, 0), // 22/12/2017 20:00 Friday
                   Exit = new DateTime(2017, 12, 23, 05, 0, 0), // 23/12/2017 05:00 Saturday
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
            List<TestDomainObject> testCases = new List<TestDomainObject>()
            {
              new TestDomainObject()
              {    //Duration 1Hour 
                  Entry = new DateTime(2017, 12, 22, 9, 0, 0),//22/12/2017 09:00 Fri
                   Exit = new DateTime(2017, 12, 22, 10, 0, 0),//22/12/2017 10:00 Fri
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 5  }
              } ,
              //Special case when duration is falling into next hour and has to be charnged for full hour
               new TestDomainObject()
              {
                   //Duration 1.20 hours
                   Entry = new DateTime(2017, 12, 22, 9, 0, 0),//22/12/2017 09:00 Fri
                   Exit = new DateTime(2017, 12, 22, 10, 20, 0),//22/12/2017 10:20 Fri
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 10  }
              } ,

              new TestDomainObject()
              { 
                   //Duration 2 hours
                   Entry = new DateTime(2017, 12, 22, 10, 0, 0),//22/12/2017 10:00 Fri
                   Exit = new DateTime(2017, 12, 22, 12, 0, 0),//22/12/2017 12:00 Fri
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 10  }
              },

              new TestDomainObject()
              { 
                   //Duration 3 hours
                   Entry = new DateTime(2017, 12, 22, 10, 0, 0),//22/12/2017 10:00 Fri
                   Exit = new DateTime(2017, 12, 22, 13, 0, 0),//22/12/2017 13:00 Fri
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 15  }
              },

              new TestDomainObject()
              {   
                   //Duration 4hours 
                   Entry = new DateTime(2017, 12, 22, 10, 0, 0),//22/12/2017 10:00 Fri
                   Exit = new DateTime(2017, 12, 22, 14, 0, 0),//22/12/2017 14:00 Fri
                   Expected = new ParkingRates { Name ="STANDARD" , Price = 20  }
              }
              ,
              new TestDomainObject()
              {   
                    //Duration 52hours
                   Entry = new DateTime(2017, 12, 22, 10, 0, 0),//22/12/2017 10:00 Fri
                   Exit = new DateTime(2017, 12, 24, 14, 0, 0),//24/12/2017 10:00 Sunday
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
