import { Component } from '@angular/core';
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

    signInForm = createSignInForm();

    constructor(private readonly authService: AuthService) {
        
    }

    signIn() {
        const credentials = this.signInForm.getRawValue() as IUserSignIn;

        this.authService.signIn(credentials)
            .pipe(take(1))
            .subscribe((data) => {
                console.log(data);
            });
    }
}
