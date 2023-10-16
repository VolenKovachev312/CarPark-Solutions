using CarParkingSystem.Contracts;
using CarParkingSystem.Data;
using CarParkingSystem.Data.Models;
using CarParkingSystemAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ParkingController(ApplicationDbContext context)
        {
            this.context= context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid? id)
        {
            var parkingLot = await context.ParkingLots.FirstOrDefaultAsync(p => p.Id == id&&p.IsDeleted==false);
            if (parkingLot == null)
            {
                return NotFound();
            }
            var parkingDto = new ParkingLotDto()
            {
                Id= parkingLot.Id,
                Name= parkingLot.Name,
                Address= parkingLot.Address,
                Capacity= parkingLot.Capacity,
                AvailableSpaces = parkingLot.AvailableSpaces,
                ImageUrl= parkingLot.ImageUrl,
                HourlyRate= parkingLot.HourlyRate,
                IsNonStop= parkingLot.IsNonStop,
                OpeningHour = parkingLot.IsNonStop ? "-" : parkingLot.OpeningHour.ToString(),
                ClosingHour = parkingLot.IsNonStop ? "-" : parkingLot.ClosingHour.ToString(),
                Latitude= parkingLot.Latitude,
                Longitude= parkingLot.Longitude
            };

            return Ok(parkingDto);
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var parkingLots = await context.ParkingLots.Where(p=>p.IsDeleted==false).ToListAsync();
            if (parkingLots == null)
            {
                return NotFound();
            }
            return Ok(parkingLots.Select(p => new ParkingLotDto()
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Capacity = p.Capacity,
                AvailableSpaces = p.AvailableSpaces,
                ImageUrl = p.ImageUrl,
                HourlyRate = p.HourlyRate,
                IsNonStop = p.IsNonStop,
                OpeningHour = p.IsNonStop ? "-" : p.OpeningHour.ToString(),
                ClosingHour = p.IsNonStop ? "-" : p.ClosingHour.ToString(),
                Latitude = p.Latitude,
                Longitude = p.Longitude
            }));
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(ParkingLotDto parkingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (context.ParkingLots.Any(p => p.Name == parkingDto.Name))
            {
                return BadRequest("Parking lot with this name already exists!");
            }
            if (context.ParkingLots.Any(p => p.Address == parkingDto.Address))
            {
                return BadRequest("Parking lot with this address already exists!");
            }
            var parkingLot = new ParkingLot()
            {
                Name= parkingDto.Name,
                Address= parkingDto.Address,
                AvailableSpaces= parkingDto.Capacity,
                Capacity= parkingDto.Capacity,
                HourlyRate= parkingDto.HourlyRate,
                ImageUrl= parkingDto.ImageUrl,
                IsNonStop= parkingDto.IsNonStop,
                OpeningHour = parkingDto.IsNonStop ? new TimeOnly(10, 0, 0) : TimeOnly.Parse(parkingDto.OpeningHour),
                ClosingHour = parkingDto.IsNonStop? new TimeOnly(10,0,0):TimeOnly.Parse(parkingDto.ClosingHour),
                Latitude = parkingDto.Latitude,
                Longitude = parkingDto.Longitude,
                IsDeleted=false
            };
            await context.ParkingLots.AddAsync(parkingLot);
            await context.SaveChangesAsync();
            return Ok(parkingDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var parkingLot= await context.ParkingLots.FirstOrDefaultAsync(p => p.Id == id&&p.IsDeleted==false);
            if(parkingLot == null)
            {
                return NotFound();
            }
            parkingLot.IsDeleted= true;
            await context.SaveChangesAsync();
            return Ok(parkingLot);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(ParkingLotDto parkingDto)
        {
            var parkingLot = await context.ParkingLots.FirstOrDefaultAsync(p => p.Id == parkingDto.Id && p.IsDeleted == false);
            if (parkingLot == null)
            {
                return NotFound();
            }

            parkingLot.Id = parkingDto.Id;
            parkingLot.Name = parkingDto.Name;
            parkingLot.Address = parkingDto.Address;
            parkingLot.AvailableSpaces = parkingDto.Capacity;
            parkingLot.Capacity = parkingDto.Capacity;
            parkingLot.HourlyRate = parkingDto.HourlyRate;
            parkingLot.ImageUrl = parkingDto.ImageUrl;
            parkingLot.IsNonStop = parkingDto.IsNonStop;
            parkingLot.OpeningHour = parkingDto.IsNonStop ? new TimeOnly(10, 0, 0) : TimeOnly.Parse(parkingDto.OpeningHour);
            parkingLot.ClosingHour = parkingDto.IsNonStop ? new TimeOnly(10, 0, 0) : TimeOnly.Parse(parkingDto.ClosingHour);
            parkingLot.Latitude = parkingDto.Latitude;
            parkingLot.Longitude = parkingDto.Longitude;
            parkingLot.IsDeleted = false;
            await context.SaveChangesAsync();
            return Ok(parkingLot);
        }
    }
}
