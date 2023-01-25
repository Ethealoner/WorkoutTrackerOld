import { WorkoutSession } from "./workout-session.model";

export interface User {
  UserId: Number;
  UserName: String;
  Email: String;
  Password: String;
  workoutSessions: WorkoutSession[];
}

export interface UserForAuthentication {
  username: string;
  password: string;
}

export interface AuthResponse {
  isAuthSuccessful: boolean;
  errorMessage: string;
  token: string;
  userId: number;
}
