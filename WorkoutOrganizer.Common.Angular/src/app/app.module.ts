import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ExerciseComponent } from './Exercise/exercise/exercise.component';
import { AppRoutingModule } from './Exercise/app-routing.module';
import { WorkoutSessionComponent } from './workout-session/workout-session.component';

@NgModule({
  declarations: [
    AppComponent,
    ExerciseComponent,
    WorkoutSessionComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
