import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { WorkoutSessionComponent } from './Components/workout-session.component';
import { ExerciseComponent } from './Components/exercise.component';


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
