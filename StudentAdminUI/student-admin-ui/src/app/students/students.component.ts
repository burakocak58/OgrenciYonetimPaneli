import { Component,ViewChild } from '@angular/core';
import { StudentsService } from './students.service';
import { Student } from '../Models/ui-models/student.models';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
@Component({
  selector: 'app-students',
  standalone: false,
  templateUrl: './students.component.html',
  styleUrl: './students.component.css'
})
export class StudentsComponent {
  students:Student[] = [];
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'dateOfBirth', 'email', 'mobile','gender',"edit"];
  dataSource : MatTableDataSource<Student> = new MatTableDataSource<Student>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  filterString ="";
  constructor(private studentsService: StudentsService) {}
  ngOnInit(): void {
  this.studentsService.getStudents().subscribe({
    
    next: (succes) => {
      
    this.students = succes;
    this.dataSource = new MatTableDataSource<Student>(this.students);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    },
    error: (err) => {
      
    }
  });
}
filterStudents(){
  this.dataSource.filter = this.filterString.trim().toLocaleLowerCase();
}
}
