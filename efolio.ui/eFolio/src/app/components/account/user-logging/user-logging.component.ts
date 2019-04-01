import { Component } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';

@Component({
  selector: 'app-user-logging',
  templateUrl: './user-logging.component.html',
  styleUrls: ['./user-logging.component.scss']
})

export class UserLoggingComponent {
  public signUp = true;

  constructor(private breakpointObserver: BreakpointObserver) { }
}
