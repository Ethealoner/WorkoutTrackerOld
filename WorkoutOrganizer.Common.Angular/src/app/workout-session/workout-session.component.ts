import { Component, OnInit } from '@angular/core';
import { WorkoutSession } from '../Models/workout-session.model';
import { WorkoutSessionService } from '../Services/workout-session.service';

@Component({
  selector: 'app-workout-session',
  templateUrl: './workout-session.component.html',
  styleUrls: ['./workout-session.component.css']
})
export class WorkoutSessionComponent implements OnInit {

  workoutSessions: WorkoutSession[] = [];
  constructor(private workoutSessionService: WorkoutSessionService) { }

  ngOnInit(): void {
    console.log("sending workoutsessions request");
    this.workoutSessionService.getAllWorkoutSessions()
      .subscribe({
        next: (workoutSession) => {
          this.workoutSessions = workoutSession;
          console.log(workoutSession);
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

}
