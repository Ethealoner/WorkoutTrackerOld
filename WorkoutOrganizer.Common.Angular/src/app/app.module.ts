import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ExerciseComponent } from './Components/exercise.component';
import { WorkoutSessionComponent } from './Components/workout-session.component';
import { WorkoutSessionDetailComponent } from './Components/workout-session-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    ExerciseComponent,
    WorkoutSessionComponent,
    WorkoutSessionDetailComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule, FormsModule, ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
