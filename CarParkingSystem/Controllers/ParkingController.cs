using CarParkingSystem.Contracts;
using CarParkingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace CarParkingSystem.Controllers
{
    public class ParkingController : Controller
    {
        private readonly IParkingService parkingService;
        public ParkingController(IParkingService parkingService)
        {
            this.parkingService = parkingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var parkingLots = await parkingService.LoadParkingLotsAsync();
            ViewBag.ParkingLots = JsonConvert.SerializeObject(parkingLots);
            return View(parkingLots);
        }
        [HttpPost]
        public IActionResult Index(IEnumerable<ParkingLotViewModel> parkingLots)
        {
            return View(parkingLots);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            ParkingLotViewModel model = new ParkingLotViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async  Task<IActionResult> Create(ParkingLotViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
               await parkingService.AddLocationAsync(model);

            }
            catch (ArgumentException ae)
            {
                ModelState.AddModelError("Name", ae.Message);
                return View(model);
            }
            return RedirectToAction("Index","Parking");
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(string name)
        {
            ParkingLotViewModel model=new ParkingLotViewModel();
            try
            {
                model = await parkingService.GetParkingLot(name);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Name", e.Message);
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string name, ParkingLotViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await parkingService.EditParkingLotAsync(name,model);
            }
            catch(Exception e)
            {

            }
            return View(model);
        }
    }
}
