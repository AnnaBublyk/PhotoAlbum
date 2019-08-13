import {TagInterface} from './tag.interface';
import {LikeInterface} from './likeInterface';

export interface PhotoInterface {
    PhotoId: number;
    ProfileId: string;
    UserName: string;
    PictureName: string;
    PicturePath: string;
    Tags: TagInterface[];
    Likes: LikeInterface[];
}
