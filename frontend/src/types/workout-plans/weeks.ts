type Week = {
  weekName: string;
  trainingProgramId: string;
  trainingProgram: {
    name: string;
    description: string;
    weeks: Array<any>;
    id: string;
  };
  workoutSessions: Array<{
    sessionName: string;
    reps: string;
    sets: string;
    restTime: string;
    exerciseId: string;
    exercise: {
      name: string;
      description: string;
      muscleGroup: number;
      videoUrl: any;
      workoutSessions: Array<any>;
      id: string;
    };
    weekName: string;
    trainingProgramId: string;
  }>;
};

export default Week;
