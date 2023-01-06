import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WorkoutSession } from '../Models/workout-session.model';
import { WorkoutSessionService } from '../Services/workout-session.service';
import { Location } from '@angular/common';
import { ExerciseService } from '../Services/exercise.service';
import { Exercise } from '../Models/exercise.model';

@Component({
  selector: 'app-workout-session-detail',
  templateUrl: './workout-session-detail.component.html',
  styleUrls: ['./workout-session-detail.component.css']
})
export class WorkoutSessionDetailComponent implements OnInit {

  @Input() workoutSession?: WorkoutSession;

  constructor(
    private workoutSessionServices: WorkoutSessionService,
    private exerciseServices: ExerciseService,
    private route: ActivatedRoute,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getWorkoutSession();
  }

  getWorkoutSession(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.workoutSessionServices.getWorkoutSession(id).subscribe(workoutSession => this.workoutSession = workoutSession);
    console.log("Workout data: " + this.workoutSession);
  }

  goBack(): void {
    this.location.back();
  }

  addExercies(id: number): void {
    var exercise: Exercise = {
        name: "New Exercise",
      typeOfExercise: "",
      repetition: "",
        workoutSessionId: id,
        id: 0
    };
    this.exerciseServices.addExercise(exercise)
      .subscribe(
        (data) => {
          this.ngOnInit();
        }
      );
  }

  deleteExercise(id: number): void {
    this.exerciseServices.deleteExercise(id)
      .subscribe(
        (data) => {
          this.ngOnInit();
        }
      );
  }
}
