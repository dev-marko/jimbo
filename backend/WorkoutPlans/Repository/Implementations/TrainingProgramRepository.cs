using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Context;
using WorkoutPlans.Domain.Relations;
using WorkoutPlans.Repository.Interfaces;

namespace WorkoutPlans.Repository.Implementations
{
    public class TrainingProgramRepository : ITrainingProgramRepository
    {
        private readonly WorkoutPlansContext context;
        private DbSet<TrainingProgramWeek> trainingProgramWeeks;

        public TrainingProgramRepository(WorkoutPlansContext context)
        {
            this.context = context;
            this.trainingProgramWeeks = context.Set<TrainingProgramWeek>();
        }

        public TrainingProgramWeek InsertTrainingProgramWeek(TrainingProgramWeek trainingProgramWeek)
        {
            var entity = trainingProgramWeeks.Add(trainingProgramWeek).Entity;
            context.SaveChanges();
            return entity;
        }

        public IEnumerable<TrainingProgramWeek> FetchWeeksForTrainingProgram(Guid trainingProgramId)
        {
            return trainingProgramWeeks
                .Where(e => e.TrainingProgramId == trainingProgramId)
                .Include(e => e.WorkoutSessions);
        }

        public TrainingProgramWeek FetchTrainingProgramWeek(Guid trainingProgramId, string weekName)
        {
            return trainingProgramWeeks
                .Include(e => e.WorkoutSessions)
                .SingleOrDefault(e => (e.TrainingProgramId == trainingProgramId) && e.WeekName.Equals(weekName));
        }

        public TrainingProgramWeek DeleteTrainingProgramWeek(TrainingProgramWeek trainingProgramWeek)
        {
            var entity = trainingProgramWeeks.Remove(trainingProgramWeek).Entity;
            context.SaveChanges();
            return entity;
        }
    }
}
