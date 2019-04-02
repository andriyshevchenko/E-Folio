import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { LoaderService } from 'src/app/services/loader.service';


@Component({
  selector: 'app-user-logging',
  templateUrl: './user-logging.component.html',
  styleUrls: ['./user-logging.component.scss']
})

export class UserLoggingComponent implements OnInit {
  signUp = true;

  constructor(private breakpointObserver: BreakpointObserver,
    private loaderService: LoaderService) { }

  ngOnInit() {
    this.loaderService.stopLoading();
  }
}
