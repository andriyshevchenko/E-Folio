import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';
import { ActivatedRoute } from '@angular/router';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-project-page',
  templateUrl: './project-page.component.html',
  styleUrls: ['./project-page.component.scss']
})
export class ProjectPageComponent implements OnInit {
  public projectInput: Project = new Project(0, '', '');

  constructor(
    private projectService: ProjectService,
    private route: ActivatedRoute,
    private loaderService: LoaderService) { }

  ngOnInit() {
    this.loaderService.startLoading();
    const id = +this.route.snapshot.paramMap.get('id');

    this.projectService.GetProject(id).subscribe(
      (res) => {
        this.projectInput = new Project(res.id, res.name, res.internalDescription);
        this.loaderService.stopLoading();
      }
    );
  }
}
