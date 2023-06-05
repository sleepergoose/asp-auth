import { FormControl, FormGroup } from "@angular/forms";

export interface IUserSignIn {
    email: string;
    password: string;
}

export const createSignInForm = () =>{
    return new FormGroup({
        email: new FormControl<string>('', {
            nonNullable: true,
        }),
        password: new FormControl<string>('', {
            nonNullable: true,
        })
    })
};

export type SignInForm = ReturnType<typeof createSignInForm>;