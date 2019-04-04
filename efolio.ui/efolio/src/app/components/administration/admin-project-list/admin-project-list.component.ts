import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatSnackBar } from '@angular/material';
import { Project } from 'src/app/models/project.model';
import { ProjectService } from 'src/app/services/project.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-admin-project-list',
  templateUrl: './admin-project-list.component.html',
  styleUrls: ['./admin-project-list.component.scss']
})
export class AdminProjectListComponent implements OnInit {
  public displayedColumns: string[] = ['id', 'name', 'description', 'actions'];
  public dataSource = new MatTableDataSource();
  public projects: Project[] = [];

  constructor(
    private projectService: ProjectService,
    private loaderService: LoaderService,
    private loginValidatorBar: MatSnackBar) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.loaderService.startLoading();
    this.projects = [];
    this.projectService.GetAll()
      .subscribe(responce => {
        responce.forEach(element => {
          this.projects.push(new Project(element.id,
            element.name,
            element.description,
            element.photoBase64));
        });
        this.dataSource.data = this.projects;
        this.loaderService.stopLoading();
      },
        (error) => console.log(error)
      );
  }

  deleteElement(id: number) {
    console.log(id);
    this.projectService.DeleteProject(id).subscribe(() =>
        this.loginValidatorBar.open('Project was deleted.', 'Ok', {
          duration: 5000,
          panelClass: ['snackBar'],
      })
    );
    this.getData();
  }
}
