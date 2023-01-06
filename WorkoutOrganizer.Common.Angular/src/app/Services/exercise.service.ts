import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { Exercise } from '../Models/exercise.model';

@Injectable({
  providedIn: 'root'
})
export class ExerciseService {

  baseApiUrl: string = environment.baseApiUrl;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getAllExercises(): Observable<Exercise[]> {
    return this.http.get<Exercise[]>(this.baseApiUrl + `api/exercise`);
  }

  getExercise(id: number): Observable<Exercise> {
    return this.http.get<Exercise>(this.baseApiUrl + `api/exercise/${id}`);
  }

  updateExercise(exercise: Exercise): Observable<any> {
    return this.http.put(this.baseApiUrl + `api/exercise/${exercise.id}`, exercise, this.httpOptions);
  }

  deleteExercise(id: number): Observable<Exercise> {
    return this.http.delete<Exercise>(this.baseApiUrl + `api/exercise/${id}`, this.httpOptions);
  }

  addExercise(exercise: Exercise): Observable<Exercise> {
    return this.http.post<Exercise>(this.baseApiUrl + `api/exercise`, exercise, this.httpOptions);
  }
}
