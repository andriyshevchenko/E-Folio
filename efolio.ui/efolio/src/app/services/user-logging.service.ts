import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as jwt_decode from "jwt-decode";
import { Observable } from 'rxjs';
import { map, catchError, retry } from 'rxjs/operators';

@Injectable()
export class UserLoggingService { 

    constructor(private httpClient: HttpClient) { }

    signIn(loginData) {
        return this.httpClient.post('http://localhost:5000/api/account/login/', loginData) 
    }

    signUp(registerData) {
        return this.httpClient.post('http://localhost:5000/api/account/register/', registerData);
    }

    isAuthenticated() : boolean {
        let token = localStorage.getItem("accessToken");
        return  token !== null && token !== undefined;
    }

    userRole(): String { 
        let role = localStorage.getItem("userRole") || "unauthorized";
        return role;
    }
}
