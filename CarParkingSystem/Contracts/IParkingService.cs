using CarParkingSystem.Models;

namespace CarParkingSystem.Contracts
{
    public interface IParkingService
    {
        Task AddLocationAsync(ParkingLotViewModel model);

        Task<IEnumerable<ParkingLotViewModel>> LoadParkingLotsAsync();

        Task<ParkingLotViewModel> GetParkingLotAsync(string name);

        Task EditParkingLotAsync(string name, ParkingLotViewModel model);

    }
}
