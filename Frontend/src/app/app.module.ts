import {BrowserModule} from '@angular/platform-browser';
import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {AuthorizationModule} from './components/authorization/authorization.module';
import {MaterialModule} from './shared/modules/material.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {ProfileModule} from './components/profile/profile.module';
import {GalleryModule} from './components/gallery/gallery.module';
import {DataService} from './shared/services/data.service';
import {HttpClientModule} from '@angular/common/http';

@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        AuthorizationModule,
        MaterialModule,
        BrowserAnimationsModule,
        ProfileModule,
        GalleryModule,
        HttpClientModule,
    ],
    providers: [ DataService ],
    bootstrap: [AppComponent],
    schemas: [
        CUSTOM_ELEMENTS_SCHEMA
    ],
})
export class AppModule {
}
