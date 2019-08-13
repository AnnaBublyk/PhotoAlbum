import {Injectable} from '@angular/core';
import {ApiService} from './api.service';
import {TagInterface} from '../interfaces/tag.interface';
import {RolesEnum} from '../enums/roles.enum';
import {UserInterface} from '../interfaces/user.interface';
import * as jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  user: UserInterface;

  tags: TagInterface[];

  constructor(
      private api: ApiService,
  ) { }

  getAllTags(): void {
    this.api.getAllTags().subscribe(
        tags => this.tags = tags,
        e => {
          console.error(e);
          this.tags = [];
        },
    );
  }

  saveUser(res: { access_token: string, username: string }): void {
    const decodedToken = jwtDecode(res.access_token);
    this.user = {
      UserName: decodedToken.UserName,
      Role: decodedToken.Role,
      IsBlocked: !!+decodedToken.IsBlocked,
      UserId: decodedToken.UserId,
    };
  }

  get isAdmin(): boolean {
    return this.user && this.user.Role === RolesEnum.Admin;
  }

  get isModerator(): boolean {
    return this.user && this.user.Role === RolesEnum.Moderator;
  }

}
