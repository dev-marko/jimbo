import Week from './weeks';

export type WorkoutPlan = {
  id: string;
  name: string;
  description: string;
  weeks: Week[];
};
