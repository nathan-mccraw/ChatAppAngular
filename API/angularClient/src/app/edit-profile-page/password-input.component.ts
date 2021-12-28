import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-password-input',
  templateUrl: './password-input.component.html',
})
export class PasswordInputComponent implements OnInit {
  @Input() inputName: string = '';
  editInput: boolean = false;
  
  constructor() { }

  ngOnInit(): void {
  }

}
