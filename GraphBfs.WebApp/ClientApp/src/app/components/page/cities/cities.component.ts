import { Component, ViewChild, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatDialog, MatTable } from '@angular/material';
import { DialogBoxComponent } from '../../elements/dialog-box/dialog-box.component';

export interface City {
  name: string;
  id: number;
}

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent {
  displayedColumns: string[] = ['id', 'name', 'action'];
  dataSource: City[];
  http: HttpClient;
  baseUrl: string;

  @ViewChild(MatTable, { static: true }) mycitytable: MatTable<any>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.http.get<City[]>(baseUrl + 'cities').subscribe(result => {
      this.dataSource = result;
    }, error => console.error(error));
  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (result.event == 'Add') {
          this.addRowData(result.data);
        } else if (result.event == 'Update') {
          this.updateRowData(result.data);
        } else if (result.event == 'Delete') {
          this.deleteRowData(result.data);
        }
      }
    });
  }

  addRowData(row_obj) {
    if (row_obj.name) {
      this.http.post<City>(this.baseUrl + 'cities', { name: row_obj.name }).subscribe(data => {
        this.dataSource.push({ ...data });
        this.mycitytable.renderRows();
      }, error => console.error(error));
    }
  }

  updateRowData(row_obj) {
    if (row_obj.name && row_obj.id) {
      this.http.put<City>(this.baseUrl + 'cities', { name: row_obj.name, id: row_obj.id }).subscribe(error => console.error(error));
      this.dataSource = this.dataSource.filter((value, key) => {
        if (value.id == row_obj.id) {
          value.name = row_obj.name;
        }
        return true;
      });
    } else {
      alert('Invalid Data!');
    }
  }

  deleteRowData(row_obj) {
    this.http.delete<City>(this.baseUrl + `cities?id=${row_obj.id}`).subscribe(error => console.error(error));
    this.dataSource = this.dataSource.filter((value, key) => {
      return value.id != row_obj.id;
    });
  }
}
