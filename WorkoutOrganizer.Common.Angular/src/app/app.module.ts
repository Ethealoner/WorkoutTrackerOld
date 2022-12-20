import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ExerciseComponent } from './Components/exercise.component';
import { WorkoutSessionComponent } from './Components/workout-session.component';

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
