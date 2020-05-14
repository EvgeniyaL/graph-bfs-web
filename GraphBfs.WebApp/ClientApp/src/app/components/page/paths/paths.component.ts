import { Component, ViewChild, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MatDialog, MatTable } from '@angular/material';
import { DialogBoxDropdownComponent } from '../../elements/dialog-box-dropdown/dialog-box-dropdown.component'

export interface Path {
  initialCity: City;
  endCity: City;
  id: number;
}

export interface City {
  name: string;
  id: number;
}

@Component({
  selector: 'app-paths',
  templateUrl: './paths.component.html',
  styleUrls: ['./paths.component.css'],
})

export class PathsComponent {
  displayedColumns: string[] = ['id', 'initialCity', 'endCity', 'action'];
  dataSource: Path[];
  http: HttpClient;
  baseUrl: string;
  @ViewChild(MatTable, { static: true }) mytable: MatTable<any>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    this.http = http;
    this.baseUrl = baseUrl;
    http.get<Path[]>(baseUrl + 'paths').subscribe(result => {
      this.dataSource = result;
    }, error => console.error(error));
  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxDropdownComponent, {
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
    if (row_obj.initialCity && row_obj.endCity) {
      this.http.post<Path>(this.baseUrl + 'paths',
        {
          initialCity: row_obj.initialCity,
          endCity: row_obj.endCity,
        }).subscribe(data => {
          this.dataSource.push({ ...data });
          this.mytable.renderRows();
        }, error => console.error(error));
    } else {
      alert('Invalid Data!');
    }
  }

  updateRowData(row_obj) {
    if (row_obj.initialCity && row_obj.endCity) {
      this.http.put<Path>(this.baseUrl + 'paths',
        {
          id: row_obj.id,
          initialCity: row_obj.initialCity,
          endCity: row_obj.endCity,
        }).subscribe(data => {
          this.dataSource = this.dataSource.filter((value, key) => {
            if (value.id == row_obj.id) {
              value.initialCity = row_obj.initialCity;
              value.endCity = row_obj.endCity;
            }
            return true;
          });
          this.mytable.renderRows();
        }, error => console.error(error));
    }
  }
  deleteRowData(row_obj) {
    this.http.delete<Path>(this.baseUrl + `paths?id=${row_obj.id}`)
      .subscribe(error => console.error(error));
    this.dataSource = this.dataSource.filter((value, key) => {
      return value.id != row_obj.id;
    });
  }
}
