import { Injectable, Output, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  // private loading: boolean;

  @Output() public showSpinner = new EventEmitter<any>();

  public startLoading() {
    this.showSpinner.emit(true);
  }

  public stopLoading() {
    this.showSpinner.emit(false);
  }
}
