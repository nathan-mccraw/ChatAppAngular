import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';

import { SignInPageComponent } from './sign-in-page/sign-in-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { EditProfilePageComponent } from './edit-profile-page/edit-profile-page.component';
import { TextInputComponent } from './edit-profile-page/text-input.component';
import { PasswordInputComponent } from './edit-profile-page/password-input.component';
import { DateInputComponent } from './edit-profile-page/date-input.component';
import { ChatPageComponent } from './chat-page/chat-page.component';


@NgModule({
  declarations: [
    AppComponent,
    SignInPageComponent,
    RegisterPageComponent,
    EditProfilePageComponent,
    TextInputComponent,
    PasswordInputComponent,
    DateInputComponent,
    ChatPageComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
