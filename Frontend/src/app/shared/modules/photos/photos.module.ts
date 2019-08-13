import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PhotosComponent} from './photos.component';
import {ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import { MaterialModule } from '../material.module';

@NgModule({
    declarations: [PhotosComponent],
    imports: [
        CommonModule,
        MaterialModule,
        ReactiveFormsModule,
        RouterModule,
    ],
    exports: [
        PhotosComponent,
    ],
})
export class PhotosModule {
}
