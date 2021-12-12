import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  @ViewChild('burger') burger: ElementRef;

  onHome() {
    console.log('Go Home');
  }

  onLogin() {
    console.log('LogIn');
  }

  onSignOut() {
    console.log('Sign Out');
  }

  onLoadMore() {
    console.log(burger);
  }
}
