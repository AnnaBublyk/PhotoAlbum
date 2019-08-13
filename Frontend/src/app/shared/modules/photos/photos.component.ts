import {Component, Input, OnInit} from '@angular/core';
import {ApiService} from '../../services/api.service';
import {FormBuilder, FormControl} from '@angular/forms';
import {PhotoInterface} from '../../interfaces/photo.interface';
import {TagInterface} from '../../interfaces/tag.interface';
import {DomSanitizer, SafeUrl} from '@angular/platform-browser';
import { DataService } from '../../services/data.service';
import {Subject} from 'rxjs';
import {LikeInterface} from '../../interfaces/likeInterface';


@Component({
    selector: 'app-photos',
    templateUrl: './photos.component.html',
    styleUrls: ['./photos.component.css']
})
export class PhotosComponent implements OnInit {
    @Input() profileId: string;
    @Input() refreshList$: Subject<void>;

    tagsCtrl: FormControl;
    photos: PhotoInterface[];

    isOldSort = false;


    constructor(
        private api: ApiService,
        private fb: FormBuilder,
        public dataService: DataService,
    ) {
    }

    ngOnInit() {
        this.dataService.getAllTags();
        this.tagsCtrl = this.fb.control([]);
        this.getPhotos();
        if (this.refreshList$) {
            this.refreshList$.subscribe(
                () => this.getPhotos(),
            );
        }
    }

    getPhotos(): void {
        this.api.getPhotos(
            this.isOldSort,
            this.tagsCtrl.value.length ? this.tagsCtrl.value : null,
            this.profileId
        ).subscribe(
            photos => this.photos = photos,
            console.log,
        );
    }


    activateSortByNew(): void {
        this.isOldSort = false;
        this.getPhotos();

    }

    activateSortByFavorite(): void {
        this.isOldSort = true;
        this.getPhotos();
    }

    applyTags(): void {
        this.getPhotos();
        this.tagsCtrl.markAsPristine();
    }

    removeImage(id: number): void {
        this.api.removePhoto(+id).subscribe(
            () => {
                alert('Фотография успешно удалена');
                this.getPhotos();
            },
            e => alert(`Произошла ошибка: ${(e.error && e.Message) || e.Message || e}`),
        );
    }

    getIsLiked(likes: LikeInterface[]): boolean {
        return this.dataService.user && !!likes.find(like => like.ProfileId === this.dataService.user.UserId);
    }

    like(PhotoId: number): void {
        this.api.like(PhotoId, this.dataService.user.UserId).subscribe(
            () => this.refreshList$.next(),
            e => alert(`Произошла ошибка: ${(e.error && e.Message) || e.Message || e}`),
        );
    }

    dislike(PhotoId: number): void {
        this.api.unlike(PhotoId, this.dataService.user.UserId).subscribe(
            () => this.refreshList$.next(),
            e => alert(`Произошла ошибка: ${(e.error && e.Message) || e.Message || e}`),
        );
    }
}
