using CarParkingSystem.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSystem.Test.Mocks
{
    public static class ParkingServiceMock
    {
        public static ParkingService Instance
        {
            get
            {
                var serviceMock=new Mock<ParkingService>();
                return serviceMock.Object;
            }
        }
    }
}
