using LGym.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LGym.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrainersController : ControllerBase
    {
        private readonly ILGymRepository _repository;

        public TrainersController(ILGymRepository repository)
        {
            _repository = repository;
        }

        // GET: api/trainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainers()
        {
          
            var trainers = await _repository.GetTrainersAsync();
            return Ok(trainers);
        }

        // GET: api/trainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trainer>> GetTrainer(int id)
        {
            var trainer = await _repository.GetTrainerAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return Ok(trainer);
        }

        // POST: api/trainers
        [HttpPost]
        public async Task<ActionResult<Trainer>> CreateTrainer(Trainer trainer)
        {
            if (trainer == null)
            {
                return BadRequest("Trainer cannot be null");
            }

            var createdTrainer = await _repository.CreateTrainerAsync(trainer);

            return CreatedAtAction(nameof(GetTrainer), new { id = createdTrainer.TrainerId }, createdTrainer);
        }

        // PUT: api/trainers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTrainer(int id, Trainer trainer)
        {
            //if (id != trainer.TrainerId)
            //{
            //    return BadRequest("Trainer ID mismatch");
            //}

            var existingTrainer = await _repository.GetTrainerAsync(id);
            if (existingTrainer == null)
            {
                return NotFound();
            }
            existingTrainer.FirstName = trainer.FirstName;
            existingTrainer.LastName = trainer.LastName;
            existingTrainer.Speciality = trainer.Speciality;
            existingTrainer.FeePer30Minutes = trainer.FeePer30Minutes;
            existingTrainer.HireDate = trainer.HireDate;
            try
            {
                await _repository.UpdateTrainerAsync(existingTrainer);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                return Conflict("The trainer data has been updated or deleted by another user.");
            }

            return NoContent();
        }
    }
}