import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.scss']
})
export class AdministrationComponent implements OnInit {
  opened = true;

  constructor(private loaderService: LoaderService) { }

  ngOnInit() {
    this.loaderService.stopLoading();
  }

}
