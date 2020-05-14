import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

export interface LogisticCenter {
  name: string;
  id: number;
}

export interface City {
  name: string;
  id: number;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  center:string;
  http: HttpClient;
  baseUrl: string;
  eventsSubject: Subject<string> = new Subject<string>();


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  calculateCenter() {
    this.http.get<LogisticCenter>(this.baseUrl + 'logisticcenter').subscribe(result => {
      if (result) {
        this.center = result.name;
        this.emitEventToChild(this.center);
      } else {
        alert('No Cities!');
      }
    }, error => console.error(error));
  }

  emitEventToChild(center) {
    this.eventsSubject.next(center);
  }
}
