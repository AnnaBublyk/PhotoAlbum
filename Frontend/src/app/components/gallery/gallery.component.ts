import {Component, OnInit} from '@angular/core';
import {DataService} from '../../shared/services/data.service';
import {Router} from '@angular/router';
import {Subject} from 'rxjs';

@Component({
    selector: 'app-gallery',
    templateUrl: './gallery.component.html',
    styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

    refreshList$: Subject<void> = new Subject();

    constructor(
        public dataService: DataService,
        private router: Router,
    ) {
    }

    ngOnInit() {
    }

    logout(): void {
        this.dataService.user = null;
        localStorage.removeItem('currentUser');
        this.router.navigate(['/login']);
    }

    login(): void {
        this.router.navigate(['/login']);
    }
}
