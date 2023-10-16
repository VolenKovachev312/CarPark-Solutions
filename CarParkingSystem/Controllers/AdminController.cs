using CarParkingSystem.Contracts;
using CarParkingSystem.Models;
using CarParkingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CarParkingSystem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IParkingService parkingService;
        private readonly IReservationService reservationService;
        private readonly IUserService userService;
        public AdminController(IParkingService parkingService, IReservationService reservationService, IUserService userService)
        {
            this.parkingService = parkingService;
            this.reservationService = reservationService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            IEnumerable<ParkingLotViewModel> parkingLots = new List<ParkingLotViewModel>();
            try
            {
                parkingLots = await parkingService.LoadParkingLotsAsync();
            }
            catch
            {
                TempData["Error"] = "Could not load parking lots!";
            }
            if (!search.IsNullOrEmpty())
            {
                ViewBag.Search = search;
            }
            else
            {
                ViewBag.Search = "";
            }
            return View(parkingLots);
        }

        public async Task<IActionResult> ParkingReservations(string parkingId)
        {
            IEnumerable<ReservationViewModel> reservations = new List<ReservationViewModel>();
            try
            {
                reservations = await reservationService.GetReservationsByParkingIdAsync(parkingId);
            }
            catch (Exception)
            {
                TempData["Error"] = "Parking not found!";
            }
            return View(reservations);
        }

        [HttpGet]
        public async Task<IActionResult> UserReservations(string searchQuery)
        {
            var userViewModel = new UserViewModel();
            try
            {
                userViewModel = await userService.GetUserReservationsAsync(searchQuery);
            }
            catch (ArgumentException ae)
            {
                TempData["Error"] = ae.Message;
                return RedirectToAction("Index", "Admin");
            }
            return View(userViewModel);
        }
    }
}
