using CarParkingSystem.Models;

namespace CarParkingSystem.Contracts
{
    public interface IParkingService
    {
        Task AddLocationAsync(ParkingLotViewModel model);

        Task<IEnumerable<ParkingLotViewModel>> LoadParkingLotsAsync();
    }
}
