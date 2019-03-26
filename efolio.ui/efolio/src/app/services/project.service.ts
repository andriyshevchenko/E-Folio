import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Project } from '../models/project.model';

@Injectable()
export class ProjectService {
    
    constructor(private http: HttpClient) { }

    GetAll() {
        let headers = new HttpHeaders();
        this.addHeaders(headers);
        return this.http.get<any>('http://localhost:5000/api/Project', {
            headers: headers
        });
    }

    addHeaders(headers: HttpHeaders) {
        headers.append('Authorization', 'kbasdlkgjbasalskfhalkdg');
        headers.append('Own-header', 'Ostap');
    }
}