import {Injectable} from '@angular/core'
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { UserLoggingService } from '../services/user-logging.service';
import { MatSnackBar } from '@angular/material';
import { LoaderService } from '../services/loader.service';

@Injectable()

export class AuthGuard implements CanActivate {
  constructor (private router: Router, 
               private authService: UserLoggingService, 
               private loader: LoaderService,
               public loginValidatorBar: MatSnackBar){

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    switch (this.authService.userRole()) {
      case "admin":
        return true;     
     
      case "unauthorized":
        this.loginValidatorBar.open('Please sign in to eFolio or create an account', 'Ok', {
          duration: 3000,
          panelClass: ['snackBar'],
        });
        this.router.navigate(["account"]);
        return false;

      default:
        this.router.navigate(["projects"]);
        this.loginValidatorBar.open('You do not have enough rights', 'Ok', {
          duration: 3000,
          panelClass: ['snackBar'],
        });
        return false;
    }
  }
}



