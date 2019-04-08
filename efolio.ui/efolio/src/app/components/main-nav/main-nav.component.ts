import { Component, HostListener, ViewChild } from '@angular/core';
import { UserLoggingService } from '../../services/user-logging.service';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.scss']
})
export class MainNavComponent {
  public showSidenav = true;
  @ViewChild('drawer') public drawer;

  constructor(private authService: UserLoggingService) { }

  public isAdmin() : boolean {
    return this.authService.userRole() === "admin";
  }

  @HostListener('window:resize', ['$event']) onResize() {
    if (window.innerWidth > 700) {
      this.drawer.close();
      this.showSidenav = false;
    }
  }
}
