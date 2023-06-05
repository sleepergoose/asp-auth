import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Observable, catchError, map, of, tap } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  return isSignedIn();
};

const isSignedIn = (): Observable<boolean> => {
    const authService = inject(AuthService);
    const router = inject(Router);

    return authService.getClaims()        
        .pipe(
            map((claims) => {
                if (claims?.length > 0) {
                    return true;
                }

                router.navigate(['auth', 'signin']);

                return false;
            }),
            catchError(() => of(false)),
        );
}
