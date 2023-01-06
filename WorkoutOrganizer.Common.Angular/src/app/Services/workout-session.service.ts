import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { WorkoutSession } from '../Models/workout-session.model';

@Injectable({
  providedIn: 'root'
})
export class WorkoutSessionService {

  baseApiUrl: string = environment.baseApiUrl;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

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

  addWorkoutSession(workoutSession: WorkoutSession): Observable<WorkoutSession> {
    return this.htpp.post<WorkoutSession>(this.baseApiUrl + `api/workoutsession`, workoutSession, this.httpOptions);
  }

  deleteWorkoutSession(id: number): Observable<WorkoutSession> {
    return this.htpp.delete<WorkoutSession>(this.baseApiUrl + `api/workoutsession/${id}`, this.httpOptions);
  }
}
