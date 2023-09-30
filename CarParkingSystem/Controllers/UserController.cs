using CarParkingSystem.Contracts;
using CarParkingSystem.Data.Models;
using CarParkingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace CarParkingSystem.Controllers
{

    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        private readonly IHttpContextAccessor context;

        private readonly IUserService userService;
        public UserController(
                UserManager<User> _userManager,
                SignInManager<User> _signInManager,
                IHttpContextAccessor _context,
                IUserService _userService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            context = _context;
            userService = _userService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> DemoAdmin()
        {
            var user = await userManager.FindByNameAsync("testadmin@gmail.com");
            var result = await signInManager.PasswordSignInAsync(user, "testpassword", false, false);

            return RedirectToAction("Index", "Admin");
        }
        [AllowAnonymous]
        public async Task<IActionResult> DemoUser()
        {
            var user = await userManager.FindByNameAsync("testuser@gmail.com");
            var result = await signInManager.PasswordSignInAsync(user, "testpassword", false, false);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
             if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Account()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var user=await userManager.FindByIdAsync(userId);
            var accountViewModel = new AccountViewModel()
            {
                Email = user.Email,
                CarMake=user.CarMake,
                CarModel=user.CarModel,
                CarNumber=user.CarNumber
            };
            return View(accountViewModel);
        }

        public async Task<IActionResult> ChangeEmail(AccountViewModel viewModel)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var newEmail = viewModel.Email;
            try
            {
                await userService.ChangeEmailAsync(userId, newEmail);
            }
            catch(ArgumentException e)
            {
                TempData["Message"] = e.Message;
                return RedirectToAction("Account");
            }
            TempData["EmailChanged"] = "Successfully changed email!";
            return RedirectToAction("Account");
        }

        public async Task<IActionResult> ChangePassword(AccountViewModel viewModel)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var oldPassword = viewModel.OldPassword;
            var newPassword = viewModel.NewPassword;
            var confirmNewPassword = viewModel.ConfirmNewPassword;
            var passValidator = new PasswordValidator<User>();
            if(string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword))
            {
                TempData["Message"] = "Fill out all password fields!";
                return RedirectToAction("Account");
            }
            var passwordValidationResult = await passValidator.ValidateAsync(userManager, null, oldPassword);

            if (!passwordValidationResult.Succeeded)
            {
                TempData["Message"] = "Current password incorrect!";
                return RedirectToAction("Account");
            }
            if (newPassword == confirmNewPassword)
            {
                var user = await userManager.FindByIdAsync(userId);
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            }
            else
            {
                TempData["Message"] = "New password does not match confirmation.";
                return RedirectToAction("Account");
            }
            TempData["PasswordChanged"] = "Successfully changed password!";
            return RedirectToAction("Account");
        }

        public async Task<IActionResult> ChangeCarInfo(AccountViewModel viewModel)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            await userService.ChangeCarInfoAsync(userId, viewModel.CarMake, viewModel.CarModel, viewModel.CarNumber);

            TempData["CarInfoChanged"] = "Successfuly changed car information!";
            return RedirectToAction("Account");
        }

        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByIdAsync(userId);
            await userManager.DeleteAsync(user);
            return RedirectToAction("Logout");
        }
    }
}
