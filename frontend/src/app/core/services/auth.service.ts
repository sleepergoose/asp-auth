import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { IUserSignIn } from '../models/IUserSignIn';
import { environment } from '../../../environments/environment';
import { IClaim } from '../models/IClaim';
import { BehaviorSubject, Subject, pipe, tap } from 'rxjs';

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

    getClaims() {
        return this.httpService.getRequest<IClaim[]>(`${this.apiUrl}/users/claims`);
    }

    getAdminData() {
        return this.httpService.getRequest<object>(`${this.apiUrl}/users/admin-data`);
    }
}
