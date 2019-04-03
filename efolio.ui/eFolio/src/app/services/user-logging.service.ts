import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as jwt_decode from "jwt-decode";
import { Observable } from 'rxjs';
import { map, catchError, retry } from 'rxjs/operators';

@Injectable()
export class UserLoggingService {
    private role: String;
    private validUntil: number; 

    constructor(private httpClient: HttpClient) { }

    signIn(loginData) {
        return this.httpClient.post('http://localhost:5000/api/account/login/', loginData)
       // .subscribe(es => {
        //     localStorage.setItem('accessToken', es["accessToken"])
        // }) 
    }

    signUp(registerData) {
        return this.httpClient.post('http://localhost:5000/api/account/register/', registerData);
    }

    isAuthenticated() : boolean {
        let token = localStorage.getItem("accessToken");
        return  token !== null && token !== undefined;
    }

    userRole(): String { 
        let date = new Date();
        let now = date.getTime();
        let userRole: String;
        
        if (!this.validUntil && !this.role) {
            let token = localStorage.getItem('accessToken');
            if (token) {
                let decoded = jwt_decode(token);
                this.role = decoded.role;
                this.validUntil = decoded.expiresIn + now;
            }
            else {
                userRole = 'unauthorized';
            }
        }
        else if (this.validUntil <= now) {
            // refresh token;
            userRole = 'unauthorized';
            localStorage.removeItem('accessToken');
            this.validUntil = null;
            this.role = null;
        }
        else{
            userRole = this.role;
        }
        return userRole;
    }
}
