using Microsoft.AspNetCore.Mvc;
using System;
using WorkoutPlans.Domain.DTO;
using WorkoutPlans.Services.Interfaces;

namespace WorkoutPlans.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }

        // EXERCISES

        [HttpGet]
        public IActionResult GetAllExercises()
        {
            return Ok(exerciseService.FetchAllExercises());
        }

        [HttpGet("{id}")]
        public IActionResult GetExercise(Guid id)
        {
            if (!exerciseService.ExerciseExists(id))
            {
                return NotFound(new { error = $"Exercise with id: {id} not found" });
            }

            return Ok(exerciseService.FetchExerciseById(id));
        }

        [HttpPost]
        public IActionResult CreateExercise([FromBody] ExerciseDTO exerciseDTO)
        {
            return Ok(exerciseService.CreateExercise(exerciseDTO));
        }

        [HttpPut("{id}")]
        public IActionResult EditExercise(Guid id, [FromBody] ExerciseDTO exerciseDTO)
        {
            if (!exerciseService.ExerciseExists(id))
            {
                return NotFound(new { error = $"Exercise with id: {id} not found" });
            }

            return Ok(exerciseService.UpdateExercise(id, exerciseDTO));
        
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExercise(Guid id)
        {
            if (!exerciseService.ExerciseExists(id))
            {
                return NotFound(new { error = $"Exercise with id: {id} not found" });
            }

            return Ok(exerciseService.DeleteExercise(id));
        }
    }
}
