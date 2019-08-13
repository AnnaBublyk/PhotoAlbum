import {PhotoInterface} from './photo.interface';
import {RolesEnum} from '../enums/roles.enum';

export interface ProfileInterface {
    ProfileId: string;
    UserId: string;
    UserName: string;
    FirstName: string;
    LastName: string;
    IsBlocked: boolean;
    Role: RolesEnum;
    PhotosDTO: PhotoInterface[];
}
