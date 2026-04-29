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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = context.MediaItems.Find(id);
            if (item == null) { return NotFound(); }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(MediaItem item)
        {
            item.lastUpdated = DateTime.UtcNow;

            context.MediaItems.Add(item);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, MediaItem updatedItem)
        {
            var item = context.MediaItems.Find(id);

            if (item == null) { return NotFound(); }

            item.Title = updatedItem.Title;
            item.MediaType = updatedItem.MediaType;
            item.Status = updatedItem.Status;
            item.Progress = updatedItem.Progress;
            item.Notes = updatedItem.Notes;
            item.ImageUrl  = updatedItem.ImageUrl;
            item.lastUpdated = DateTime.UtcNow;

            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = context.MediaItems.Find(id);

            if (item == null) { return NotFound(); }

            context.MediaItems.Remove(item);
            context.SaveChanges();

            return NoContent() ;
        }
    }
}
