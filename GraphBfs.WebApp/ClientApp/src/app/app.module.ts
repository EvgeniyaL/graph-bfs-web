import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/page/home/home.component';
import { CitiesComponent } from './components/page/cities/cities.component';
import { PathsComponent } from './components/page/paths/paths.component';
import { DialogBoxComponent } from './components/elements/dialog-box/dialog-box.component';
import { DialogBoxDropdownComponent } from './components/elements/dialog-box-dropdown/dialog-box-dropdown.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NetworkGraphComponent } from './components/page/home/network-graph/network-graph.component';
import { HighchartsChartModule } from 'highcharts-angular';

import {
  MatTableModule,
  MatDialogModule,
  MatFormFieldModule,
  MatInputModule,
  MatButtonModule,
  MatSelectModule,
} from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CitiesComponent,
    PathsComponent,
    DialogBoxComponent,
    DialogBoxDropdownComponent,
    NetworkGraphComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'cities', component: CitiesComponent },
      { path: 'paths', component: PathsComponent },
      { path: 'dialog-box', component: DialogBoxComponent },
      { path: 'dialog-box-dropdown', component: DialogBoxDropdownComponent },
    ]),
    BrowserAnimationsModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    HighchartsChartModule
  ],
  entryComponents: [
    DialogBoxComponent,
    DialogBoxDropdownComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
