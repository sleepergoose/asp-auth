import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, take } from 'rxjs';
import { IClaim } from 'src/app/core/models/IClaim';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent {
    claims$: Observable<IClaim[]> = this.authService.getClaims();

    adminData: any;

    constructor(
        private readonly authService: AuthService,
        private readonly router: Router,
    ) {

    }

    getAdminData() {
        this.authService.getAdminData()
            .pipe(take(1))
            .subscribe((data) => {
                this.adminData = data;
            });
    }

    logOut() {
        this.authService.signOut().subscribe(
            () => {
                this.router.navigate(['auth', 'signin']);
            }
        );
    }
}
