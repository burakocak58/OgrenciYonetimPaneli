import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../Models/api-models/student.model';
import { updateStudentRequest } from '../Models/api-models/updateStudentRequest.models';
import { AddStudentRequest } from '../Models/api-models/AddStudentRequest.model.ts';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  private baseApiUrl = "https://localhost:44354"

  constructor(private httpClient:HttpClient) { }
  getStudents() : Observable<Student[]> {
    return this.httpClient.get<Student[]>(this.baseApiUrl + "/Students");
  }
   getStudent(studentId:string | null) : Observable<Student> {
    return this.httpClient.get<Student>(this.baseApiUrl + "/students/" + studentId);
  }
  UpdateStudent(studentId:string,studentRequest:Student) : Observable<Student> {
    const updateStudentRequest : updateStudentRequest= {
      firstName: studentRequest.firstName,
      lastName: studentRequest.lastName,
      dateOfBirth: studentRequest.dateOfBirth,
      email: studentRequest.email,
      mobile: studentRequest.mobile,
      genderId: studentRequest.genderId,
      physicalAddress: studentRequest.address.physicalAddress,
      postalAddress: studentRequest.address.postalAddress

    }
    return this.httpClient.put<Student>(this.baseApiUrl + "/students/" + studentId,updateStudentRequest);
  }
   DeleteStudent(studentId:string) : Observable<Student>{
    
    return this.httpClient.delete<Student>(this.baseApiUrl + "/students/" + studentId);
   }
     addStudent(studentRequest: Student): Observable<Student> {
const addStudentRequest: AddStudentRequest = {
  firstName: studentRequest.firstName,
  lastName: studentRequest.lastName,
  dateOfBirth: new Date(studentRequest.dateOfBirth).toISOString(),  // ISO string
  email: studentRequest.email,
  mobile: studentRequest.mobile.toString(),                          // string olması lazım
  genderId: studentRequest.genderId,
  physicalAdress: studentRequest.address.physicalAddress,            // backend’de “Adress”
  postalAdress: studentRequest.address.postalAddress
};


  // ✅ DÜZELTİLMİŞ satır:
  return this.httpClient.post<Student>(this.baseApiUrl + "/students/add", addStudentRequest);
}
getImagePath(relativePath:string)
{
  return `${this.baseApiUrl}/${relativePath}`;
}
 uploadImage(studentId:string,file:File) : Observable<any> {
    const formData = new FormData();
    formData.append('profileImage', file);
    return this.httpClient.post(this.baseApiUrl + "/students/" + studentId + "/upload-image",formData,{responseType: 'text'});
  }


}
