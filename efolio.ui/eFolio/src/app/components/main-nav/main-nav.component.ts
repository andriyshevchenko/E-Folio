import { Component, HostListener, ViewChild } from '@angular/core';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.scss']
})
export class MainNavComponent {
  public showSidenav = true;
  @ViewChild('drawer') public drawer;

  constructor() { }

  @HostListener('window:resize', ['$event']) onResize() {
    if (window.innerWidth > 700) {
      this.drawer.close();
      this.showSidenav = false;
    }
  }
}
