import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { DataService } from '../../services/data.service';

@Component({
    selector: 'app-add-new-category',
    templateUrl: './add-new-category.component.html',
    styleUrls: ['./add-new-category.component.css']
})
export class AddNewCategoryComponent implements OnInit {

    addingCategoryInProgress: boolean;

    newCategoryCtrl: FormControl;

    constructor(
        private api: ApiService,
        private fb: FormBuilder,
        private dataService: DataService,
    ) {
    }

    ngOnInit() {
        this.newCategoryCtrl = this.fb.control('', Validators.required);
    }

    addCategory(): void {
        this.addingCategoryInProgress = true;
    }

    saveNewCategory(): void {
        this.addingCategoryInProgress = false;
        this.api.addCategory(this.newCategoryCtrl.value).subscribe(
            () => {
                alert('Категория успешно сохранена');
                this.dataService.getAllTags();
            },
            e => alert(`Произошла ошибка: ${(e.error && e.Message) || e.Message || e}`),
        );
    }

    cancelAddingCategory(): void {
        this.addingCategoryInProgress = false;
    }

}
