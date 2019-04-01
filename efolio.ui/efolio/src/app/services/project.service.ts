import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class ProjectService {

    constructor(private http: HttpClient) { }

    GetAll() {
        const httpHeaders = new HttpHeaders();
        this.addHeaders(httpHeaders);
        return this.http.get<any>('http://localhost:5000/api/Project', {
            headers: httpHeaders
        });
    }

    addHeaders(headers: HttpHeaders) {
        headers.append('Authorization', 'kbasdlkgjbasalskfhalkdg');
        headers.append('Own-header', 'Ostap');
    }
}
