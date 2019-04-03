import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdministrationService {

  constructor(private http: HttpClient) { }

  getAllUsers() {
    const httpHeaders = new HttpHeaders();
    this.addHeaders(httpHeaders);
    return this.http.get('http://localhost:5000/api/admin', {
      headers: httpHeaders
    });
  }
  addHeaders(headers: HttpHeaders) {
    headers.append('Authorization', 'qwertyasdfgzxvc');
    headers.append('Own-header', 'Name');
  }
}
