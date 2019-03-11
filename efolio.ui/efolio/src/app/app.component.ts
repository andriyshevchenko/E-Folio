import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  private router: Router;

  constructor(router: Router) {
    this.router = router;
  }

  ngOnInit(): void {
    this.router.navigate(['signin']);
  }

  title = 'efolio';
}
