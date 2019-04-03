import {Injectable} from '@angular/core'
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { UserLoggingService } from '../services/user-logging.service';
import { MatSnackBar } from '@angular/material';

@Injectable()

export class AuthGuard implements CanActivate {
  constructor (private router: Router, private authService: UserLoggingService, public loginValidatorBar: MatSnackBar){

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    switch (this.authService.userRole()) {
      case "admin":
        return true;     

      case "unauthorized":
        this.loginValidatorBar.open('Please sign in to eFolio or create account', 'Ok', {
          duration: 3000,
          panelClass: ['snackBar'],
        });
        this.router.navigate(["account"]);
        return false;
      default:
        return false;
    }
  }
}



