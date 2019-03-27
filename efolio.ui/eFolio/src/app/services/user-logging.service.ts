import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable()
export class UserLoggingService {
    constructor(private httpClient: HttpClient) { }

    signIn(loginData) {
        return this.httpClient.post('http://localhost:5000/api/account/login/', loginData);
    }

    signUp(registerData) {
        return this.httpClient.post('http://localhost:5000/api/account/register/', registerData);
    }
}
