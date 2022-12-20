import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ExerciseComponent } from './exercise/exercise.component';
import { WorkoutSessionComponent } from '../workout-session/workout-session.component';

const routes: Routes = [
  { path: '', component: WorkoutSessionComponent },
  { path: "exercise", component: ExerciseComponent },
  { path: "workoutSession",component: WorkoutSessionComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
