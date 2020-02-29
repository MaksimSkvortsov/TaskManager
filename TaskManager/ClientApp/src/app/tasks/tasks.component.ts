import { Component, OnInit } from '@angular/core';
import { TaskService } from '../task.service';
import { Task } from '../task';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  providers: [TaskService]
})
export class TasksComponent {

  tasks: Task[] = [];
  newTask: Task = new Task();

  constructor(private taskService: TaskService) { }

  ngOnInit() {
    this.getAllTasks();
  }
  
  getAllTasks() {
    this.taskService.getAll()
      .subscribe(t => this.tasks = t);
  }

  addTask() {
    this.taskService.add(this.newTask)
      .subscribe(t => this.tasks.push(t));
    this.newTask = new Task();
  }

  toggleTaskCompletion(task: Task) {
    task.isCompleted = !task.isCompleted;
    this.taskService.update(task)
      .subscribe(
        () => console.log(`task ${task.id} updated`),
        error => console.log(error)
      );
  }

  removeTask(task: Task) {
    this.taskService.delete(task.id)
      .subscribe(
        () => { 
          this.tasks.splice(this.tasks.indexOf(task, 0), 1); 
          console.log(`task ${task.id} deleted`);
        },
        error => console.log(error)
      );
  }
}
