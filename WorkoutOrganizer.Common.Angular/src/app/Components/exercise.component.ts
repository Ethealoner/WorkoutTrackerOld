import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Exercise } from '../Models/exercise.model';
import { ExerciseService } from '../Services/exercise.service';
import { Location } from '@angular/common';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

export interface RepsForm {
  repsNumber: number;
  repsDifficulty: number;
}

@Component({
  selector: 'app-exercise',
  templateUrl: './exercise.component.html',
  styleUrls: ['./exercise.component.css']
})

export class ExerciseComponent implements OnInit {

  @Input() exercise?: Exercise;

  exerciseTypesForms = new FormControl('');
  exerciseTypes: string[] = ['weight', 'distance'];

  exerciseRepetitionForms = this.formBuilder.group({
    reps: this.formBuilder.array([])
  });

  exerciseTypeLabel1?: string;
  exerciseTypeLabel2?: string;
  exerciseTypeLabel3?: string;

  constructor(
    private exerciseServices: ExerciseService,
    private route: ActivatedRoute,
    private location: Location,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.getExercise();

    this.exerciseTypesForms.valueChanges
      .subscribe(ch => {
        this.onExerciseTypeChanged(ch!);
      });
  }

  getExercise(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.exerciseServices.getExercise(id).subscribe(exercise => {
      this.exercise = exercise;
      this.recreateRepsInForms();
      this.setExerciseLabels();
    })
  }

  goBack(): void {
    this.location.back();
  }
  save(): void {
    if (this.exercise) {
      this.getRepsFromForms(this.reps.value);
      this.exerciseServices.updateExercise(this.exercise).subscribe();
    }
  }

  getRepsFromForms(repsForm: RepsForm[]): void {
    if (this.exercise) {
      this.exercise.repetition = JSON.stringify(repsForm);
    }

  }

  onExerciseTypeChanged(value: string): void {
    if (this.exercise) {
      this.exercise.typeOfExercise = value;
      this.setExerciseLabels();
    }
  }

  setExerciseLabels(): void {
    if (this.exercise) {
      if (this.exercise.typeOfExercise == "weight") {
        this.exerciseTypeLabel1 = "Rep";
        this.exerciseTypeLabel2 = "weight";
        this.exerciseTypeLabel3 = "KG";
      } else {
        this.exerciseTypeLabel1 = "Distance(M)";
        this.exerciseTypeLabel2 = "time";
        this.exerciseTypeLabel3 = "MIN";
      }
    }
  }
  get reps() {
    return this.exerciseRepetitionForms.get('reps') as FormArray;
  }

  addExerciseRep(): void {
    const exerciseGroup = this.formBuilder.group({
      repsNumber: [1, Validators.required],
      repsDifficulty: [1, Validators.required]
    });

    this.reps.push(exerciseGroup);
  }

  removeExerciseRep(repIndex: number): void {
    this.reps.removeAt(repIndex);
  }

  recreateRepsInForms(): void {
    if (this.exercise) {
      let repsForm: RepsForm[];
      repsForm = JSON.parse(this.exercise.repetition);
      for (let rep of repsForm) {
        let exerciseGroup = this.formBuilder.group({
          repsNumber: [rep.repsNumber, Validators.required],
          repsDifficulty: [rep.repsDifficulty, Validators.required]
        });
        this.reps.push(exerciseGroup);
      }
    }
  }

}
