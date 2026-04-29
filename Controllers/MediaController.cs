using Microsoft.AspNetCore.Mvc;
using MediaTrackerAPI.Data;
using MediaTrackerAPI.Models;
using System.Linq;

namespace MediaTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MediaController : ControllerBase
    {
        private readonly AppDbContext context;

        public MediaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = context.MediaItems.ToList();
            return Ok(items);
        }

        [HttpPost]
        public IActionResult Create(MediaItem item)
        {
            context.MediaItems.Add(item);
            item.lastUpdated = DateTime.UtcNow;
            context.SaveChanges();

            return Ok(item);
        }
    }
}
