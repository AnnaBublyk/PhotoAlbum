import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ProfileComponent} from './profile.component';
import {MaterialModule} from '../../shared/modules/material.module';
import {PhotosModule} from '../../shared/modules/photos/photos.module';
import {AddNewCategoryModule} from '../../shared/modules/add-new-category/add-new-category.module';
import { AddPhotoDialogComponent } from './components/add-photo-dialog/add-photo-dialog.component';
import {ReactiveFormsModule} from '@angular/forms';

@NgModule({
    declarations: [
        ProfileComponent,
        AddPhotoDialogComponent,
    ],
    imports: [
        CommonModule,
        MaterialModule,
        PhotosModule,
        AddNewCategoryModule,
        ReactiveFormsModule,
    ],
    entryComponents: [ AddPhotoDialogComponent ],
})
export class ProfileModule {
}
