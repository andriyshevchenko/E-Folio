import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-contact-info',
  templateUrl: './contact-info.component.html',
  styleUrls: ['./contact-info.component.scss']
})

export class ContactInfoComponent {

  public contactinfoForm: FormGroup;

  constructor() { 
    this.contactinfoForm = new FormGroup({});
  }
}
