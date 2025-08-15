// src/app/services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44354'; // Buraya kendi API adresini yaz

  constructor(private http: HttpClient) {}

 login(credentials: { userName: string; password: string }): Observable<any> {
  return this.http.post(`${this.apiUrl}/api/auth/login`, credentials);
  }
}
