import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Task } from './task';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private apiUrl = 'https://localhost:5002/api/ToDoTasks';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getAll() : Observable<Task[]>{
    return this.http.get<Task[]>(this.apiUrl);
  }

  add(task: Task): Observable<Task> {
    return this.http.post<Task>(this.apiUrl, task, this.httpOptions);
  }

  delete (taskId: number): Observable<any> {
    let url = `${this.apiUrl}/${taskId}`;
    return this.http.delete(url);
  }

  update (task: Task): Observable<any> {
    let url = `${this.apiUrl}/${task.id}`;
    return this.http.put(url, task, this.httpOptions);
  }
}
