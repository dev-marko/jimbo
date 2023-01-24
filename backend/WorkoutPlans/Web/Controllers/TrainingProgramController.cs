using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public TrainingProgramController(ITrainingProgramService trainingProgramService, IExerciseService exerciseService)
        {
            this.trainingProgramService = trainingProgramService;
            this.exerciseService = exerciseService;

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
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
            return Ok(exerciseService.FetchWorkoutSessionsForWeek(trainingProgramWeekDTO));
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
            }

            return Ok("test");
        }

        [HttpPut("week")]
        public IActionResult UpdateTrainingProgramWeek([FromBody] TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            // Don't forget to add OldWeekName to the DTO
            // Don't forget to add OldExerciseId, OldName, OldWeekName & TrainingProgramId properties to DTOs
            exerciseService.DeleteMultipleWorkoutSessions(trainingProgramWeekDTO.WorkoutSessions);
            trainingProgramService.UpdateTrainingProgramWeek(trainingProgramWeekDTO);
            exerciseService.CreateWorkoutSessionsForListOfExercises(trainingProgramWeekDTO.WorkoutSessions);
             
            return Ok("test");
        }

        [HttpDelete("week")]
        public IActionResult DeleteTrainingProgramWeek([FromBody] TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            // Don't forget to add OldWeekName to the DTO
            return Ok(trainingProgramService.DeleteTrainingProgramWeek(trainingProgramWeekDTO));
        }

        [HttpGet("{id}/download")]
        public IActionResult DownloadTrainingProgram(Guid id)
        {
            var trainingProgram = trainingProgramService.FetchTrainingProgramById(id);
            var weeks = trainingProgramService.FetchAllWeeksForTrainingProgram(id);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "TrainingProgram.doc");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{Name}}", trainingProgram.Name);
            document.Content.Replace("{{Description}}", trainingProgram.Description);

            StringBuilder sb = new StringBuilder();
            StringBuilder final = new StringBuilder();

            foreach (var week in weeks)
            {
                sb.AppendLine(week.WeekName);
                foreach (var workout in week.WorkoutSessions)
                {
                    String temp = sb.ToString();
                    if (!temp.Contains(workout.SessionName))
                        sb.AppendLine($"    {workout.SessionName}:");
                    sb.AppendLine($"        - {workout.Exercise.Name}: {workout.Sets} sets {workout.Reps} reps");
                }
                final.AppendLine(sb.ToString());
                sb.Clear();
            }

            document.Content.Replace("{{Program}}", final.ToString());

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "Test.pdf");
        }
    }
}
