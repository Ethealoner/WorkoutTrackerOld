import { Component, OnInit } from '@angular/core';
import { Exercise } from '../Models/exercise.model';
import { ExerciseService } from '../Services/exercise.service';

@Component({
  selector: 'app-exercise',
  templateUrl: './exercise.component.html',
  styleUrls: ['./exercise.component.css']
})
export class ExerciseComponent implements OnInit {

  exercises: Exercise[] = [];
  constructor(private exerciseServices: ExerciseService) { }

  ngOnInit(): void {
    console.log("sending exercises request");
    this.exerciseServices.getAllExercises()
      .subscribe({
        next: (exercise) => {
          this.exercises = exercise;
          console.log(exercise);
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

}
