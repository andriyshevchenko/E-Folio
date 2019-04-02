import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';
import { LoaderService } from 'src/app/services/loader.service';
@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {
  public projects: Project[] = [];

  constructor(private projectService: ProjectService, private loaderService: LoaderService) { }

  ngOnInit() {
    this.loaderService.startLoading();
    this.projectService.GetAll().subscribe(
      (res) => {
        res.forEach(element => {
          this.projects.push(new Project(element.name, element.internalDescription));
        });
        this.loaderService.stopLoading();
      }

    );
  }
}
