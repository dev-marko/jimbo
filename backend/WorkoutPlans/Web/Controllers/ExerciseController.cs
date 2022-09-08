using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // WORKOUT SESSIONS
        // (ovie celi metodi najverojatno ke treba da se prefrlat vo training program controller)
        // odnosno barem sozdavanjeto na workout sessions for exercise ke treba da bide vo
        // training program controller

        [HttpPost("workout-session")]
        public IActionResult CreateWorkoutSessionForExercise([FromBody] WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            // You have to send the Exercise ID in the body
            if (!exerciseService.ExerciseExists(workoutSessionForExerciseDTO.ExerciseId))
            {
                return NotFound(new { error = $"Exercise with id: {workoutSessionForExerciseDTO.ExerciseId} not found" });
            }

            return Ok(exerciseService.CreateWorkoutSessionForExercise(workoutSessionForExerciseDTO));
        }

        [HttpPut("workout-session")]
        public IActionResult EditWorkoutSessionForExercise([FromBody] WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            // When editing a workout session, always add the old session name and old exercise id,
            // even if they haven't changed!!
            if (!exerciseService.WorkoutSessionForExerciseExists(workoutSessionForExerciseDTO))
            {
                return NotFound(new { error = $"Workout session for exercise not found" });
            }

            return Ok(exerciseService.UpdateWorkoutSessionForExercise(workoutSessionForExerciseDTO));
        }

        [HttpDelete("workout-session")]
        public IActionResult DeleteWorkoutSessionForExercise([FromBody] WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            // When deleting a workout session, always add the old session name and old exercise id,
            // even if they haven't changed!!
            if (!exerciseService.WorkoutSessionForExerciseExists(workoutSessionForExerciseDTO))
            {
                return NotFound(new { error = $"Workout session with for exercise not found" });
            }

            return Ok(exerciseService.DeleteWorkoutSessionForExercise(workoutSessionForExerciseDTO));
        }
    }
}
