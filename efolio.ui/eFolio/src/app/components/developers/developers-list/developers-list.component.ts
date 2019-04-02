import { Component, OnInit } from '@angular/core';
import { Response } from '@angular/http';
import { DeveloperServiceService as DeveloperService } from 'src/app/services/developer-service.service';
import { Developer } from 'src/app/components/models/developer.model';
import { MatProgressSpinnerModule } from '@angular/material';
import { SpinnerComponent } from 'src/app/components/spinner/spinner.component';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-developers-list',
  templateUrl: './developers-list.component.html',
  styleUrls: ['./developers-list.component.scss']
})

export class DevelopersListComponent implements OnInit {
  developers: Developer[] = [];

  constructor(private developerService: DeveloperService, 
    private loaderService: LoaderService) { }

  ngOnInit() {
    this.loaderService.startLoading();
    this.developerService.getAllDevelopers()
      .subscribe(response => {
        this.getData(response);
        this.loaderService.stopLoading();
      },
        (error) => console.log(error)
      );
  }
  private getData(response) {
    response.forEach(element => {
      this.developers.push(new Developer(element.id, 
        element.fullName, 
        element.internalCV, 
        element.photoBase64));
    });
  }
}
