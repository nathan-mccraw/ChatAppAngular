import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { WelcomePageComponent } from './welcome-page/welcome-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { EditProfilePageComponent } from './edit-profile-page/edit-profile-page.component';
import { TextInputComponent } from './edit-profile-page/text-input.component';
import { PasswordInputComponent } from './edit-profile-page/password-input.component';
import { DateInputComponent } from './edit-profile-page/date-input.component';


@NgModule({
  declarations: [
    AppComponent,
    WelcomePageComponent,
    SignInPageComponent,
    RegisterPageComponent,
    EditProfilePageComponent,
    TextInputComponent,
    PasswordInputComponent,
    DateInputComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, ReactiveFormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
