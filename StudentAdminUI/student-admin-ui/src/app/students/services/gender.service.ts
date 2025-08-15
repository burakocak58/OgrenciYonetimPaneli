import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Gender } from '../../Models/api-models/gender.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenderService {
 private baseApiUrl = "https://localhost:44354"
  constructor(private httpClient:HttpClient) { }
  getGenderList() : Observable<Gender[]> {
      return this.httpClient.get<Gender[]>(this.baseApiUrl + "/genders");
    }
}
