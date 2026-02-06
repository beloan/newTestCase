using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HistoryApi.Data;
using HistoryApi.DTOs;

namespace HistoryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoriesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public HistoriesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get(
            int page = 1,
            int pageSize = 20,
            string? filterText = null,
            string? filterUser = null,
            string? filterEventTypeName = null,
            int? filterEventType = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            string? sortBy = null,
            bool desc = false

        )



        {
            var query = _db.Histories
                .Include(h => h.User)
                .Include(h => h.EventType)
                .AsQueryable();
            Console.WriteLine($"filterEventType = {filterEventType}");

            // Фильтры
            if (!string.IsNullOrEmpty(filterText))
                query = query.Where(x => x.Text.Contains(filterText));

            if (!string.IsNullOrEmpty(filterUser))
                query = query.Where(x => x.User.FullName.Contains(filterUser));

            if (!string.IsNullOrEmpty(filterEventTypeName))
            {
                query = query.Where(h => h.EventType.Name == filterEventTypeName);
            }

            if (dateFrom.HasValue)
                query = query.Where(h => h.Dt >= dateFrom.Value);





            // Сортировка
            query = sortBy switch
            {
                "id" => desc ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id),

                "text" => desc ? query.OrderByDescending(x => x.Text)
                               : query.OrderBy(x => x.Text),

                "user" => desc ? query.OrderByDescending(x => x.User.FullName)
                               : query.OrderBy(x => x.User.FullName),

                "date" => desc ? query.OrderByDescending(x => x.Dt)
                               : query.OrderBy(x => x.Dt),

                "eventType" => desc ? query.OrderByDescending(x => x.EventType.Name)
                                    : query.OrderBy(x => x.EventType.Name),

                _ => query.OrderByDescending(x => x.Dt)
            };

            var totalItems = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(h => new HistoryFilterDto
                {
                    Id = h.Id,
                    Text = h.Text,
                    UserFullName = h.User.FullName,
                    Dt = h.Dt,
                    EventTypeId = h.EventTypeId,
                    EventTypeName = h.EventType.Name
                })
                .ToListAsync();

            return Ok(new
            {
                totalItems,
                page,
                pageSize,
                data
            });
        }
    }
}

