using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ApiDZ.Models;
using System.Threading.Tasks;
using System;

namespace ApiDZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        CommentContext db;
        public CommentsController(CommentContext context)
        {
            db = context;
            if (!db.Comments.Any())
            {
                db.Comments.Add(new Comment { Category = "Tom", ProductName = "Auto", Rating = 5, CommentText = "Super" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> Get()
        {
            return await db.Comments.ToListAsync();
        }

        // GET api/comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> Get(Guid id)
        {
            Comment comment = await db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
                return NotFound();
            return new ObjectResult(comment);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Comment>> Post(Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            db.Comments.Add(comment);
            await db.SaveChangesAsync();
            return Ok(comment);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Comment>> Put(Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }
            if (!db.Comments.Any(x => x.Id == comment.Id))
            {
                return NotFound();
            }

            db.Update(comment);
            await db.SaveChangesAsync();
            return Ok(comment);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> Delete(Guid id)
        {
            Comment comment = db.Comments.FirstOrDefault(x => x.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            db.Comments.Remove(comment);
            await db.SaveChangesAsync();
            return Ok(comment);
        }
    }
}
