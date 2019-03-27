import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.scss']
})
export class FeedbackComponent implements OnInit {

public feedbackForm = new FormGroup({
    name: new FormControl(null),
  email: new FormControl(null, [Validators.email, Validators.required]),
  message: new FormControl(null, [Validators.required])
})


  constructor() { }

  ngOnInit() {
  }

}
