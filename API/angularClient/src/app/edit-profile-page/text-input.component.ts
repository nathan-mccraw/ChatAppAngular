import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
})
export class TextInputComponent implements OnInit {
  @Input() inputName: string = '';
  editInput: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

}
