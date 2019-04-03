import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material';
import { LoaderService } from 'src/app/services/loader.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent implements OnInit, OnDestroy {
  public loading = true;
  public loadingSubscription: any;

  constructor(private loaderService: LoaderService) { }
  ngOnInit() {
    this.loadingSubscription = this.loaderService.showSpinner
      .subscribe(value => {
        this.loading = value;
      });
  }

  ngOnDestroy() {
    this.loadingSubscription.unsubscribe();
  }
}
