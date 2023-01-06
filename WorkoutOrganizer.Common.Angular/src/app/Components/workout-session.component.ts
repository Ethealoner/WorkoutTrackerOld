import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { WorkoutSession } from '../Models/workout-session.model';
import { WorkoutSessionService } from '../Services/workout-session.service';

@Component({
  selector: 'app-workout-session',
  templateUrl: './workout-session.component.html',
  styleUrls: ['./workout-session.component.css'],
  providers: [DatePipe]
})
export class WorkoutSessionComponent implements OnInit {

  workoutSessions: WorkoutSession[] = [];
  currentDate = new Date();
  constructor(private workoutSessionService: WorkoutSessionService, private datePipe: DatePipe) {
    this.datePipe.transform(this.currentDate, 'yyyy-MM-dd');
  }

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

  addWorkoutSession(): void {
    var workoutSession: WorkoutSession = {
        workoutSessionId: 0,
      workoutScore: 0,
      workoutDate: this.currentDate.toISOString(),
        exercises: []
    };

    this.workoutSessionService.addWorkoutSession(workoutSession)
      .subscribe (
        (data) => {
          this.ngOnInit();
        }
      );
  }

  deleteWorkoutSession(id: number): void {
    this.workoutSessionService.deleteWorkoutSession(id)
      .subscribe(
        (data) => {
          this.ngOnInit();
        }
      );
  }

}
