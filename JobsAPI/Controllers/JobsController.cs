using JobsAPI.Entities;
using JobsAPI.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly JobsDbContext _context;
        public JobsController(JobsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var jobs = _context.Jobs.ToList();

            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var job = _context.Jobs.SingleOrDefault(j => j.Id == id);

            if(job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        [HttpPost]
        public IActionResult Post(Job job)
        {
            _context.Jobs.Add(job);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = job.Id}, job);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Job inputedJob)
        {
            var job = _context.Jobs
                .SingleOrDefault(j => j.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            job.Update(inputedJob.Title, inputedJob.Description, inputedJob.Company, inputedJob.Location, inputedJob.Salary);

            _context.Jobs.Update(job);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var job = _context.Jobs
                .SingleOrDefault(j => j.Id == id);

            if (job == null)
            {
                return NotFound();
            }
            _context.Jobs.Remove(job);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
