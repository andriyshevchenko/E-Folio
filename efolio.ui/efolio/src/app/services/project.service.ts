import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Project } from '../models/project.model';

@Injectable()
export class ProjectService {
    
    constructor(private http: HttpClient) { }

    getAll() {
        let headers = new HttpHeaders(); 
        return this.http.get<any>('http://localhost:5000/api/Project', {
            headers: headers
        });
    } 
}