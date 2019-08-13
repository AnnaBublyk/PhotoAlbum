import {Component, OnInit} from '@angular/core';
import {DataService} from '../../../../shared/services/data.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
    selector: 'app-add-photo-dialog',
    templateUrl: './add-photo-dialog.component.html',
    styleUrls: ['./add-photo-dialog.component.css']
})
export class AddPhotoDialogComponent implements OnInit {

    tagsCtrl: FormControl;

    selectedFile: File;

    constructor(
        public dataService: DataService,
        private fb: FormBuilder,
    ) {
    }

    ngOnInit() {
        this.tagsCtrl = this.fb.control([]);
    }

    onFileSelected(event: any): void {
        this.selectedFile = event.target.files[0];
    }

}
