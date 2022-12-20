import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { Exercise } from '../Models/exercise.model';

@Injectable({
  providedIn: 'root'
})
export class ExerciseService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getAllExercises(): Observable<Exercise[]> {
    return this.http.get<Exercise[]>(this.baseApiUrl + 'api/exercise')
  }
}
