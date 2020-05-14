import { Component, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { HttpClient } from '@angular/common/http';

export interface UsersData {
  initialCity: City;
  endCity: City;
  id: number;
}

export interface City {
  name: string;
  id: number;
}

@Component({
  selector: 'app-dialog-box-dropdown',
  templateUrl: './dialog-box-dropdown.component.html'
})
export class DialogBoxDropdownComponent {

  action: string;
  local_data: any;
  initialCities: City[];
  endCities: City[];

  constructor(
    http: HttpClient, @Inject('BASE_URL') baseUrl: string,
    public dialogRef: MatDialogRef<DialogBoxDropdownComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: UsersData) {
    this.local_data = { ...data };
    this.action = this.local_data.action;
    http.get<City[]>(baseUrl + 'cities').subscribe(result => {
      this.initialCities = result;
      this.endCities = result;
    }, error => console.error(error));
  }

  doAction() {
    this.dialogRef.close({ event: this.action, data: this.local_data });
  }

  closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }
}
