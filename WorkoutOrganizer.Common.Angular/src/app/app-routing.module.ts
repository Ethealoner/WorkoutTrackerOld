import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { WorkoutSessionComponent } from './Components/workout-session.component';
import { ExerciseComponent } from './Components/exercise.component';
import { WorkoutSessionDetailComponent } from './Components/workout-session-detail.component';
import { LoginComponent } from './Components/login.component';
import { AuthGuard } from './auth.guard';
import { RegistrationComponent } from './Components/registration.component';


const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: "exercise", component: ExerciseComponent, canActivate: [AuthGuard] },
  { path: "workoutSession", component: WorkoutSessionComponent, canActivate: [AuthGuard] },
  { path: "exercise/:id", component: ExerciseComponent, canActivate: [AuthGuard] },
  { path: "workoutSessionDetail/:id", component: WorkoutSessionDetailComponent, canActivate: [AuthGuard] },
  { path: "register", component: RegistrationComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
