namespace CarParkingSystem.Services
{
    using CarParkingSystem.Contracts;
    using CarParkingSystem.Data;
    using CarParkingSystem.Models;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        public UserService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task ChangeCarInfoAsync(string userId, string licensePlateNumber)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            user.LicensePlateNumber = licensePlateNumber;
            await context.SaveChangesAsync();
        }

        public async Task ChangeEmailAsync(string userId, string email)
        {
            var user= await context.Users.FirstOrDefaultAsync(u=>u.Id.ToString() == userId);
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Can not use blank space as email.");
            }
            if (context.Users.Any(u => u.Email == email))
            {
                throw new ArgumentException("Email already in use.");
            }
            if (user.Email!=email)
            {
                user.Email= email;
                user.UserName = email;
                user.NormalizedEmail = email.ToUpper();
                user.NormalizedUserName= email.ToUpper();
                await context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Email already in use.");
            }
        }

        public async Task<UserViewModel> GetUserViewModelAsync(string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            return new UserViewModel() 
            {
                Email=user.Email,
                FirstName=user.FirstName,
                LastName=user.LastName,
                LicensePlateNumber=user.LicensePlateNumber,
                PhoneNumber=user.PhoneNumber,
            };
        }
    }
}
