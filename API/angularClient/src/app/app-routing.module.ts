import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatPageComponent } from './chat-page/chat-page.component';
import { EditProfilePageComponent } from './edit-profile-page/edit-profile-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { SignInPageComponent } from './sign-in-page/sign-in-page.component';

const routes: Routes = [
  {path: '', component: SignInPageComponent},
  {path: 'editProfile', component: EditProfilePageComponent},
  {path: 'register', component: RegisterPageComponent},
  {path: 'chat/:id', component: ChatPageComponent},
  {path: '**', redirectTo: '',  pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
