import { Component, Input } from '@angular/core';
import { Project } from '../../../../models/project.model';

@Component({
  selector: 'app-project-item',
  templateUrl: './project-item.component.html',
  styleUrls: ['./project-item.component.scss']
})
export class ProjectItemComponent {
  @Input() public projectInput: Project;

  constructor() { }
}
