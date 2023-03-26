using Eventmi.Core.Models;

namespace Eventmi.Core.Contracts
{
    public interface IEventService
    {
        Task AddAsync(EventFormModel model);

        Task DeleteAsync(int id);

        Task UpdateAsync(EventFormModel model);

        Task<EventFormModel> GetEventAsync(int id);

        Task<IEnumerable<EventFormModel>> GetEventsAsync();
    }
}
