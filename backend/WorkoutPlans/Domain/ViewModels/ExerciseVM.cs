using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlans.Domain.ViewModels
{
    public class ExerciseVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MuscleGroup { get; set; }
        public string VideoUrl { get; set; } // video tutorial showing you how the exercise is performed
    }
}
