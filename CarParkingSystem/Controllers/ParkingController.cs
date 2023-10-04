using CarParkingSystem.Contracts;
using CarParkingSystem.Data.Models;
using CarParkingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Web.Helpers;

namespace CarParkingSystem.Controllers
{
    public class ParkingController : Controller
    {
        private readonly IParkingService parkingService;
        private readonly IUserService userService;
        public ParkingController(IParkingService parkingService,IUserService userService)
        {
            this.parkingService = parkingService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var parkingLots = await parkingService.LoadParkingLotsAsync();
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
        public async Task<IActionResult> Create(ParkingLotViewModel model)
        {
            if (!ModelState.IsValid)
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
            return RedirectToAction("Index", "Parking");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string nameToEdit)
        {
            ParkingLotViewModel model = new ParkingLotViewModel();
            try
            {
                model = await parkingService.GetParkingLotAsync(nameToEdit);
                ViewBag.Name = nameToEdit;
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("Name", e.Message);
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ParkingLotViewModel model,string nameToEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await parkingService.EditParkingLotAsync(nameToEdit, model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("EditError", e.Message);
            }
            return RedirectToAction("Index","Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Reserve(string parkingName)
        {
            ReserveViewModel model = new ReserveViewModel();
            try
            {
                var parkingLot= await parkingService.GetParkingLotAsync(parkingName);

                model.ParkingLotViewModel = parkingLot;
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    var user = await userService.GetUserViewModelAsync(userId);
                    model.UserViewModel = user;
                   
                }
                
            }
            catch (Exception e)
            {
                TempData["Message"] = e.Message;
                return RedirectToAction("Index", "Parking");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Reserve(ReserveViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.CheckInHour<DateTime.Now)
            {
                ModelState.AddModelError("CheckInHour", $"Check-in date and time must be past {DateTime.Now}!");
                return View(model);
            }
            if (model.CheckInHour > model.CheckOutHour)
            {
                ModelState.AddModelError("CheckInHour", "Check-out time can't be earlier than check-in!");
                return View(model);
            }
            var openDate = model.CheckInHour.Date;
            openDate += model.ParkingLotViewModel.OpeningHour.ToTimeSpan();

            var closeDate = model.CheckOutHour.Date;
            closeDate += model.ParkingLotViewModel.ClosingHour.ToTimeSpan();
            if(model.ParkingLotViewModel.OpeningHour>model.ParkingLotViewModel.ClosingHour)
            {
                closeDate.AddDays(1);
            }
            if(model.ParkingLotViewModel.IsNonStop==false&&(model.CheckInHour<openDate ||model.CheckOutHour>closeDate))
            {
                ModelState.AddModelError("CheckInHour", "Please select hours during the parking lot's work time!");
                return View(model);
            }
            try
            {
                await parkingService.CreateReservationAsync(model);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return View(model);
            }
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Account", "User");
            }
            TempData["Messsage"] = "Successfully made a reservation!";
            return RedirectToAction("Index", "Home");
        }
    }
}
