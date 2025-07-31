using Application.Workshops.DTOs;

namespace Application.Workshops.Interfaces
{
    public interface IWorkshopService
    {
        Task<List<WorkshopDto>> GetActiveWorkshopsAsync();
        Task<bool> ExistsAsync(int workshopId);
    }
}
