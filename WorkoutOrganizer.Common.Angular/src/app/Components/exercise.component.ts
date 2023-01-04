import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Exercise } from '../Models/exercise.model';
import { ExerciseService } from '../Services/exercise.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-exercise',
  templateUrl: './exercise.component.html',
  styleUrls: ['./exercise.component.css']
})
export class ExerciseComponent implements OnInit {

  @Input() exercise?: Exercise;

  constructor(
    private exerciseServices: ExerciseService,
    private route: ActivatedRoute,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getExercise();
  }

  getExercise(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.exerciseServices.getExercise(id).subscribe(exercise => this.exercise = exercise);
  }

  goBack(): void {
    this.location.back();
  }
  save(): void {
    if (this.exercise) {
      this.exerciseServices.updateExercise(this.exercise).subscribe();
    }
  }

}
