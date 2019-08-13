import {Component, OnInit} from '@angular/core';
import {DataService} from './shared/services/data.service';
import {Router} from '@angular/router';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    constructor(
        private dataService: DataService,
    ) {
    }

    ngOnInit(): void {
        const currentUser = JSON.parse(localStorage.getItem('currentUser'));

        if (currentUser) {
            this.dataService.saveUser(currentUser);
        }
    }
}
