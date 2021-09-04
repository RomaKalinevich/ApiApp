using ApiDZ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiDZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        CommentContext db;
        public UsersController(CommentContext context)
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

        // GET api/users/5
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
        public async Task<ActionResult<Comment>> Post(Comment user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.Comments.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Comment>> Put(Comment user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Comments.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> Delete(Guid id)
        {
            Comment user = db.Comments.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Comments.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
