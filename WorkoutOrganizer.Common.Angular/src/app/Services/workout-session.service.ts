import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { WorkoutSession } from '../Models/workout-session.model';

@Injectable({
  providedIn: 'root'
})
export class WorkoutSessionService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private htpp: HttpClient) { }

  getAllWorkoutSessions(): Observable<WorkoutSession[]> {
    return this.htpp.get<WorkoutSession[]>(this.baseApiUrl + 'api/workoutsession');
  }

  getWorkoutSession(id: number): Observable<WorkoutSession> {
     this.htpp.get<WorkoutSession>(this.baseApiUrl + `api/workoutsession/${id}`)
      .subscribe(data => {
        console.log(data);
      });
    return this.htpp.get<WorkoutSession>(this.baseApiUrl + `api/workoutsession/${id}`);
  }
}
