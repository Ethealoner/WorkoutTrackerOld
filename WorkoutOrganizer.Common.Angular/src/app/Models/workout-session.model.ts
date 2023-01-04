import { Exercise } from "./exercise.model";

export interface WorkoutSession {
  workoutSessionId: number;
  workoutScore: number;
  workoutDate: string;
  exercises: Exercise[];
}
