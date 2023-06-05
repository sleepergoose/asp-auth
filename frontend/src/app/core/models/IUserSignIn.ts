import { FormControl, FormGroup, Validators } from "@angular/forms";

export interface IUserSignIn {
    email: string;
    password: string;
}

export const createSignInForm = () =>{
    return new FormGroup({
        email: new FormControl<string>('', {
            nonNullable: true,
            validators: [
                Validators.required,
                Validators.email
            ],
        }),
        password: new FormControl<string>('', {
            nonNullable: true,
            validators: [
                Validators.required,
                Validators.pattern('^[a-zA-Z0-9-_!@#$%^&]{8,20}$'),
                Validators.minLength(8)
            ],
        })
    })
};

export type SignInForm = ReturnType<typeof createSignInForm>;