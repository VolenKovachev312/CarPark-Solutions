using CarParkingSystem.Contracts;
using CarParkingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CarParkingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IParkingService parkingService;
        public AdminController(IParkingService parkingService)
        {
            this.parkingService = parkingService;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var parkingLots = await parkingService.LoadParkingLotsAsync();
        //    return View(parkingLots);
        //}
        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var parkingLots = await parkingService.LoadParkingLotsAsync();
            if(!search.IsNullOrEmpty())
            {
                ViewBag.Search = search;
            }
            else
            {
                ViewBag.Search = "";
            }
            return View(parkingLots);
        }
        
    }
}
