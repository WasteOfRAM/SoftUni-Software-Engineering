using Eventmi.Core.Contracts;
using Eventmi.Core.Models;
using Eventmi.Data.Common.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Eventmi.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> repository;

        public EventService(IRepository<Event> _repository)
        {
            this.repository = _repository;
        }

        public async Task AddAsync(EventFormModel model)
        {
            Event entity = new Event() 
            {
                Name = model.Name,
                Start = model.Start,
                End = model.End,
                Place = model.Place,
            };

            await this.repository.AddAsync(entity);
            await this.repository.SaveChangesAsync();
        }

        public async Task<EventFormModel> GetEventAsync(int id)
        {
            Event eventModel = await this.repository.GetByIdAsync(id);

            EventFormModel eventFormModel = new EventFormModel()
            {
                Id = eventModel.Id,
                Name = eventModel.Name,
                Start = eventModel.Start,
                End = eventModel.End,
                Place = eventModel.Place,
            };

            return eventFormModel;
        }

        public async Task<IEnumerable<EventFormModel>> GetEventsAsync()
        {
            return await this.repository.AllAsNoTracking()
                .Select(e => new EventFormModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start,
                    End = e.End,
                    Place = e.Place,
                })
                .ToListAsync();
        }

        public async Task UpdateAsync(EventFormModel model)
        {
            var entity = await this.repository.GetByIdAsync(model.Id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid id", nameof(model.Id));
            }

            entity.Name = model.Name;
            entity.Start = model.Start;
            entity.End = model.End;
            entity.Place = model.Place;

            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Event entity = await this.repository.GetByIdAsync(id);

            this.repository.Delete(entity);

            await this.repository.SaveChangesAsync();
        }
    }
}
