import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { HomeComponent } from './components/home/home.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    MatCardModule,
    MatButtonModule,
  ]
})
export class MainModule { }
