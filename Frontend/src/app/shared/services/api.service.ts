import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {TagInterface} from '../interfaces/tag.interface';
import {PhotoInterface} from '../interfaces/photo.interface';
import {AddPhotoRequestInterface} from '../interfaces/add-photo-request.interface';
import {ProfileInterface} from '../interfaces/profile.interface';
import {SignupRequestInterface} from '../interfaces/signup-request.interface';
import {LoginRequestInterface} from '../interfaces/login-request.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  serverAddress = 'http://localhost:64918/api/';

  constructor(
      private http: HttpClient,
  ) { }

  signUp(payload: SignupRequestInterface): Observable<void> {
    return this.http.post<void>(`${this.serverAddress}/signup`, payload);
  }

  logIn(payload: LoginRequestInterface): Observable<{access_token: string, username: string}> {
    return this.http.post<{access_token: string, username: string}>(`${this.serverAddress}/login`, payload);
  }


  getPhotos(isOld = false, masTag?: number[], profileId = '0'): Observable<PhotoInterface[]> {
    return this.http.post<PhotoInterface[]>(
        `${this.serverAddress}photos/get`,
        { isOld, masTag, profileId },
      );
  }

  getAllTags(): Observable<TagInterface[]> {
    return this.http.get<TagInterface[]>(`${this.serverAddress}tags/get`);
  }

  addCategory(Name: string): Observable<void> {
    return this.http.post<void>(`${this.serverAddress}/tags/post`, { Name });
  }

  removePhoto(PhotoId: number): Observable<void> {
    return this.http.post<void>(`${this.serverAddress}/photos/delete`, { PhotoId });
  }

  blockUser(ProfileId: string): Observable<void> {
    return this.http.post<void>(`${this.serverAddress}/user/block`, { ProfileId });
  }

  unBlockUser(ProfileId: string): Observable<void> {
    return this.http.post<void>(`${this.serverAddress}/user/unblock`, { ProfileId });
  }

  savePhoto(payload: AddPhotoRequestInterface): Observable<{PhotoId: number}> {
    const formData = new FormData();
    formData.append('file', payload.file, payload.file.name);
    return this.http.post<{PhotoId: number}>(`${this.serverAddress}/photo/add/${payload.profileId}`, formData);
  }

  addTagsToPhoto(Tags: number[], PhotoId: number): Observable<void> {
    return this.http.post<void>(`${this.serverAddress}/photo/tags/add`, { Tags, PhotoId });
  }

  like(PhotoId: number, ProfileId: string): Observable<void> {
    return this.http.post<void>(`${this.serverAddress}/photo/like`, { ProfileId, PhotoId });
  }

  unlike(PhotoId: number, ProfileId: string): Observable<void> {
    return this.http.post<void>(`${this.serverAddress}/photo/unlike`, { ProfileId, PhotoId });
  }

  getProfileDetail(ProfileId: string): Observable<ProfileInterface> {
    return this.http.post<ProfileInterface>(`${this.serverAddress}/profile/info/get`, { ProfileId });
  }
}
