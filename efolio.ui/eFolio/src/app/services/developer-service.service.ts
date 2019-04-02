import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Developer } from '../components/models/developer.model';

@Injectable()
export class DeveloperServiceService {

  constructor(private http: HttpClient) { }

  getAllDevelopers(){
    let headers = new HttpHeaders();
    this.addHeaders(headers);
    return this.http.get<Developer>('http://localhost:5000/api/developers',{
      headers: headers
    })
  }

  // getOneDeveloper(){
  //   let headers = new HttpHeaders();
  //   this.addHeaders(headers);
  //   return this.http.get<Developer>('');
  // }

  addHeaders(headers: HttpHeaders){
    headers.append('Authorization', 'qwertyasdfgzxvc');
    headers.append('Own-header', 'Name');
  }
}
