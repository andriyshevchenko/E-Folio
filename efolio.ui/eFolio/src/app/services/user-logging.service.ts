import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
@Injectable()
export class UserLoggingService {
    constructor(private http: Http) { }

    signIn(loginData) {
        return this.http.post('http://localhost:5000/api/account/login/', loginData);
    }

    signUp(registerData) {
        return this.http.post('http://localhost:5000/api/account/register/', registerData);
    }
}