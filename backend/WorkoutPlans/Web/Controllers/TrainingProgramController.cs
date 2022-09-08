﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.DTO;
using WorkoutPlans.Domain.Relations;
using WorkoutPlans.Services.Interfaces;

namespace WorkoutPlans.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingProgramController : ControllerBase
    {
        private readonly ITrainingProgramService trainingProgramService;
        private readonly IExerciseService exerciseService;
        private readonly ISessionForWeekService sessionForWeekService;

        public TrainingProgramController(ITrainingProgramService trainingProgramService, IExerciseService exerciseService, ISessionForWeekService sessionForWeekService)
        {
            this.trainingProgramService = trainingProgramService;
            this.exerciseService = exerciseService;
            this.sessionForWeekService = sessionForWeekService;
        }

        [HttpGet]
        public IActionResult GetAllTrainingPrograms()
        {
            return Ok(trainingProgramService.FetchAllTrainingPrograms());
        }

        [HttpGet("{id}/weeks")]
        public IActionResult GetAllWeeksForTrainingProgram(Guid id)
        {
            if (!trainingProgramService.TrainingProgramExists(id))
            {
                return NotFound(new { error = $"Training program with id: {id} not found" });
            }

            // You get the weeks with workout sessions for the week and exercise included
            return Ok(trainingProgramService.FetchAllWeeksForTrainingProgram(id));
        }

        [HttpGet("workout-sessions")]
        public IActionResult GetAllWorkoutSessionsForWeek([FromBody] TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            if (!trainingProgramService.TrainingProgramExists(trainingProgramWeekDTO.TrainingProgramId))
            {
                return NotFound(new { error = $"Training program with id: {trainingProgramWeekDTO.TrainingProgramId} not found" });
            }

            // You get just the workout sessions with the exercise included
            return Ok(sessionForWeekService.FetchAllWorkoutSessionsForWeek(trainingProgramWeekDTO));
        }

        [HttpGet("{id}")]
        public IActionResult GetTrainingProgram(Guid id)
        {
            if (!trainingProgramService.TrainingProgramExists(id))
            {
                return NotFound(new { error = $"Training program with id: {id} not found" });
            }

            return Ok(trainingProgramService.FetchTrainingProgramById(id));
        }

        [HttpPost]
        public IActionResult CreateTrainingProgram([FromBody] TrainingProgramDTO trainingProgramDTO)
        {
            return Ok(trainingProgramService.CreateTrainingProgram(trainingProgramDTO));
        }

        [HttpPut("{id}")]
        public IActionResult EditTrainingProgram(Guid id, [FromBody] TrainingProgramDTO trainingProgramDTO)
        {
            if (!trainingProgramService.TrainingProgramExists(id))
            {
                return NotFound(new { error = $"Training program with id: {id} not found" });
            }

            return Ok(trainingProgramService.UpdateTrainingProgram(id, trainingProgramDTO));
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteTrainingProgram(Guid id)
        {
            if (!trainingProgramService.TrainingProgramExists(id))
            {
                return NotFound(new { error = $"Training program with id: {id} not found" });
            }

            return Ok(trainingProgramService.DeleteTrainingProgram(id));
        }

        [HttpPost("week")]
        public IActionResult CreateTrainingProgramWeek([FromBody] TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            trainingProgramService.CreateTrainingProgramWeek(trainingProgramWeekDTO);

            if (trainingProgramWeekDTO.WorkoutSessions != null)
            {
                exerciseService.CreateWorkoutSessionsForListOfExercises(trainingProgramWeekDTO.WorkoutSessions);
                sessionForWeekService.CreateMultipleWorkoutSessionsForWeek(trainingProgramWeekDTO);
            }

            return Ok("test");
        }

        [HttpPut("week")]
        public IActionResult UpdateTrainingProgramWeek([FromBody] TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            List<SessionForWeek> oldSessions = sessionForWeekService.FetchAllWorkoutSessionsForWeek(trainingProgramWeekDTO);

            trainingProgramService.UpdateTrainingProgramWeek(trainingProgramWeekDTO);
            exerciseService.UpdateWorkoutSessionsForListOfExercises(trainingProgramWeekDTO.WorkoutSessions);
            sessionForWeekService.UpdateMultipleWorkoutSessionsForWeek(oldSessions, trainingProgramWeekDTO);

            return Ok("test");
        }

        [HttpDelete("week")]
        public IActionResult DeleteTrainingProgramWeek([FromBody] TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            return Ok(trainingProgramService.DeleteTrainingProgramWeek(trainingProgramWeekDTO));
        }
    }
}
