import {Component, OnInit} from '@angular/core';
import {DataService} from '../../shared/services/data.service';
import {ActivatedRoute, Router} from '@angular/router';
import {ApiService} from '../../shared/services/api.service';
import {MatDialog} from '@angular/material';
import {AddPhotoDialogComponent} from './components/add-photo-dialog/add-photo-dialog.component';
import {filter, switchMap} from 'rxjs/operators';
import {Subject} from 'rxjs';
import {AddPhotoRequestInterface} from '../../shared/interfaces/add-photo-request.interface';
import {ProfileInterface} from '../../shared/interfaces/profile.interface';
import {RolesEnum} from '../../shared/enums/roles.enum';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

    profileId: string;

    profileInfo: ProfileInterface;

    refreshList$: Subject<void> = new Subject();

    roleEnum = RolesEnum;

    constructor(
        public dataService: DataService,
        private route: ActivatedRoute,
        private api: ApiService,
        private router: Router,
        private dialog: MatDialog,
    ) {
        this.profileId = this.route.snapshot.paramMap.get('profileId');
    }

    ngOnInit() {
        this.api.getProfileDetail(this.profileId).subscribe(
            res => this.profileInfo = res,
        );
    }

    blockUser(): void {
        this.api.blockUser(this.profileId).subscribe(
            () => {
                alert('Пользователь успешно заблокирован');
                this.profileInfo.IsBlocked = true;
            },
            e => alert(`Произошла ошибка: ${(e.error && e.Message) || e.Message || e}`),
        );
    }

    unBlockUser(): void {
        this.api.unBlockUser(this.profileId).subscribe(
            () => {
                alert('Пользователь успешно разблокирован');
                this.profileInfo.IsBlocked = false;
            },
            e => alert(`Произошла ошибка: ${(e.error && e.Message) || e.Message || e}`),
        );
    }

    startAddPhoto(): void {
        this.dialog.open(AddPhotoDialogComponent).afterClosed().pipe(
            filter(res => !!res),
        ).subscribe(
            res => this.savePhoto({...res, profileId: this.profileId}),
        );
    }

    savePhoto(req: AddPhotoRequestInterface): void {
        this.api.savePhoto(req).pipe(
            switchMap(res => this.api.addTagsToPhoto(req.Tags, res.PhotoId)),
        ).subscribe(
            () => {
                alert('Фотография успешно сохранена');
                this.refreshList$.next();
            },
            e => alert(`Произошла ошибка: ${(e.error && e.Message) || e.Message || e}`)
        );
    }

    logout(): void {
        this.dataService.user = null;
        localStorage.removeItem('currentUser');
        this.router.navigate(['/login']);
    }

    login(): void {
        this.router.navigate(['/login']);
    }

    goToGallery(): void {
        this.router.navigate(['/gallery']);
    }
}
