import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { IUserSignIn, createSignInForm } from 'src/app/core/models/IUserSignIn';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent {
    hide: boolean = true;

    errorMessage: string = '';

    signInForm = createSignInForm();

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router,
    ) {
        
    }

    signIn() {
        this.errorMessage = '';

        if (this.signInForm.invalid) {
            return;
        }

        const credentials = this.signInForm.getRawValue() as IUserSignIn;

        this.authService.signIn(credentials)
            .pipe(take(1))
            .subscribe({
                next: () => {
                    this.router.navigate(['home']);
                },
                error: (err) => {
                    if (err instanceof HttpErrorResponse) {
                        if (err.status === 4010) {
                            this.errorMessage = 'Incorrect credentials!';
                        }
                    }
                },
            });
    }
}
