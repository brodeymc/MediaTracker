using MediaTrackerAPI.Data;
using MediaTrackerAPI.DTOs;
using MediaTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MediaTrackerAPI.Controllers
{
    [Authorize]
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
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var items = context.MediaItems.Where(x => x.UserId == userId).ToList();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var item = context.MediaItems.Find(id);
            if (item == null) { return NotFound(); }
            if (item.UserId != userId) { return Unauthorized(); }

            return Ok(item);
        }

        [HttpGet("status/{status}")]
        public IActionResult GetByStatus(string status)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var items = context.MediaItems.Where(x => x.Id == userId).Where(x => x.Status == status).ToList();

            return Ok(items);
        }

        [HttpPost]
        public IActionResult Create(CreateMediaDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var item = new MediaItem
            {
                Title = dto.Title,
                MediaType = dto.MediaType,
                Status = dto.Status,

                Progress = "0%",
                Notes = "",
                ImageUrl = "",
                UserId = userId,
                lastUpdated = DateTime.UtcNow
            };

            context.MediaItems.Add(item);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, MediaItem updatedItem)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (id != updatedItem.Id) { return BadRequest(); }

            var item = context.MediaItems.Find(id);

            if (item == null) { return NotFound(); }

            if (item.UserId != userId) { return Unauthorized(); }

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
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var item = context.MediaItems.Find(id);

            if (item == null) { return NotFound(); }

            if (item.UserId != userId) { return Unauthorized(); }

            context.MediaItems.Remove(item);
            context.SaveChanges();

            return NoContent() ;
        }
    }
}
