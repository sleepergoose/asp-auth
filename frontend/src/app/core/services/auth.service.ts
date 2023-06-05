import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { IUserSignIn } from '../models/IUserSignIn';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
    private readonly apiUrl: string = environment.apiUrl;

    constructor(private readonly httpService: HttpInternalService) { }

    signIn(credentials: IUserSignIn) {
        return this.httpService.postRequest(`${this.apiUrl}/auth/sign-in`, credentials);
    }

    signOut() {
        return this.httpService.getRequest(`${this.apiUrl}/auth/sign-out`);
    }
}
