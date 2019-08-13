import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {GalleryComponent} from './gallery.component';
import {PhotosModule} from '../../shared/modules/photos/photos.module';
import {AddNewCategoryModule} from '../../shared/modules/add-new-category/add-new-category.module';
import {MaterialModule} from '../../shared/modules/material.module';

@NgModule({
    declarations: [GalleryComponent],
    imports: [
        CommonModule,
        PhotosModule,
        AddNewCategoryModule,
        MaterialModule,
    ],
    exports: [GalleryComponent],
})
export class GalleryModule {
}
