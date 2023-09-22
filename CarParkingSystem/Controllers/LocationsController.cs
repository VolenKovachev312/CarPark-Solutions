using CarParkingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingSystem.Controllers
{
    public class LocationsController : Controller
    {
        public IActionResult Index(IEnumerable<ParkingLotViewModel> parkingLots)
        {
            return View(parkingLots);
        }
    }
}
