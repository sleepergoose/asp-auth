import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

const routes: Routes = [
    {
        path: 'auth',
        loadChildren: () => (import('./modules/auth/auth.module')).then(m => m.AuthModule),
    },
    {
        path: 'home',
        loadChildren: () => (import('./modules/main/main.module')).then(m => m.MainModule),
        canActivate: [authGuard],
    },
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
    },
    {
        path: '**',
        redirectTo: 'home',
        pathMatch: 'full',
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
