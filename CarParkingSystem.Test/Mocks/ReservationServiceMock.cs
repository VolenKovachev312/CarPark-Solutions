using CarParkingSystem.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSystem.Test.Mocks
{
    public static class ReservationServiceMock
    {
        public static ReservationService Instance
        {
            get
            {
                var reservationService=new Mock<ReservationService>();
                return reservationService.Object;
            }
        }
    }
}
