using Autofac;
using Rover.Services.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Model;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Rover.ServicesTests
{
    [TestClass()]
    public class RoverServiceTests :RoverServiceTestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void GetRoverCoordinatesFromApiTest()
        {
            int recordCount = 0;
            string RoverNavigationInput = "5 5\r\n1 2 N\r\nLMLMLMLMM";
            RoverOutputModel expectedResult = new RoverOutputModel()
            { PositionX = 1, PositionY = 3, Heading = "N" };

            using (var container = builder.Build())
            {
                var RoverNavigationService = container.Resolve<IRoverNavigationService>();
                var model = RoverNavigationService.GetRoverCoordinates(RoverNavigationInput);
                recordCount = model.Count;
                if (model!= null && model.Count > 0)
                {
                    var result = model[0];
                    Assert.AreEqual(expectedResult.PositionX, result.PositionX);
                    Assert.AreEqual(expectedResult.PositionY, result.PositionY);
                    Assert.AreEqual(expectedResult.Heading, result.Heading);
                }
            }
        }
    }
}
