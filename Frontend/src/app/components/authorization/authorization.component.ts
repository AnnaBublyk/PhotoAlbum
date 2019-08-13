import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {ApiService} from '../../shared/services/api.service';
import {DataService} from '../../shared/services/data.service';

@Component({
    selector: 'app-authorization',
    templateUrl: './authorization.component.html',
    styleUrls: ['./authorization.component.css']
})
export class AuthorizationComponent implements OnInit {

    isLogin: boolean;

    loginForm: FormGroup;
    signUpForm: FormGroup;

    constructor(
        public fb: FormBuilder,
        private router: Router,
        private api: ApiService,
        private dataService: DataService,
    ) {
    }

    ngOnInit() {
        this.isLogin = true;
        this.createLoginForm();
        this.createSignUpForm();
    }

    startSignUp(): void {
        this.isLogin = false;
    }

    logIn(): void {
        this.api.logIn(this.loginForm.value).subscribe(
            res => {
                localStorage.setItem('currentUser', JSON.stringify(res));
                this.dataService.saveUser(res);
                this.router.navigate([`/profile/${this.dataService.user.UserId}`]);
            },
            e => alert(`Произошла ошибка: ${(e.error && e.error.Message) || e.Message || e}`),
        );
    }

    signUp(): void {
        this.api.signUp(this.signUpForm.value).subscribe(
            () => {
                alert('Пользователь успешно зарегистрирован');
                this.isLogin = true;
            },
            e => alert(`Произошла ошибка: ${(e.error && e.Message) || e.Message || e}`),
        );
    }

    createLoginForm(): void {
        this.loginForm = this.fb.group({
            UserName: [
                '',
                Validators.required,
            ],
            Password: [
                '',
                [Validators.required, Validators.minLength(6)],
            ]
        });
    }

    createSignUpForm(): void {
        this.signUpForm = this.fb.group({
            FirstName: [
                '',
                [Validators.required, Validators.minLength(3)],
            ],
            LastName: [
                '',
                [Validators.required, Validators.minLength(3)],
            ],
            UserName: [
                '',
                [Validators.required, Validators.minLength(3)],
            ],
            Password: [
                '',
                [Validators.required, Validators.minLength(6)],
            ],
            ConfirmPassword: [
                '',
                [
                    Validators.required,
                    this.validatorDifferentPasswords.bind(this),
                ],
            ],
        });
    }

    validatorDifferentPasswords(control: FormControl): { [error: string]: boolean } | null {
        if (!this.signUpForm) {
            return;
        }
        return this.passwordCtrl.value !== this.recheckPasswordCtrl.value ? {differentPasswords: true} : null;
    }

    get recheckPasswordCtrl(): AbstractControl {
        return this.signUpForm.get('ConfirmPassword');
    }

    get passwordCtrl(): AbstractControl {
        return this.signUpForm.get('Password');
    }
}
