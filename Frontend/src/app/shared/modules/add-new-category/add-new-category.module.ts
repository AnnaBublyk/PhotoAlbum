import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AddNewCategoryComponent} from './add-new-category.component';
import {MaterialModule} from '../material.module';
import {ReactiveFormsModule} from '@angular/forms';

@NgModule({
    declarations: [AddNewCategoryComponent],
    imports: [
        CommonModule,
        MaterialModule,
        ReactiveFormsModule
    ],
    exports: [ AddNewCategoryComponent ],
})
export class AddNewCategoryModule {
}
