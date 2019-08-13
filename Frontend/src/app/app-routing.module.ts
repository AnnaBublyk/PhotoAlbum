import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {GalleryComponent} from './components/gallery/gallery.component';
import {ProfileComponent} from './components/profile/profile.component';
import {AuthorizationComponent} from './components/authorization/authorization.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'gallery',
    pathMatch: 'full'
  },
  {
    path: 'gallery',
    component: GalleryComponent,
  },
  {
    path: 'profile/:profileId',
    component: ProfileComponent
  },
  {
    path: 'login',
    component: AuthorizationComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
