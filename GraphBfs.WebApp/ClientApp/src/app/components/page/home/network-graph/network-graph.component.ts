import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import * as Highcharts from "highcharts";
import HighchartsNetworkgraph from "highcharts/modules/networkgraph";
import HighchartsExporting from "highcharts/modules/exporting";
import { Subscription, Observable } from 'rxjs';

HighchartsNetworkgraph(Highcharts);
HighchartsExporting(Highcharts);

export interface Path {
  initialCity: City;
  endCity: City;
  id: number;
}

export interface City {
  name: string;
  id: number;
}

export interface Node {
  color: string;
  marker: Marker;
  id: string;
}

export interface Marker {
  radius: number;
}

@Component({
  selector: 'network-graph',
  templateUrl: './network-graph.component.html',
  styleUrls: ['./network-graph.component.css']
})
export class NetworkGraphComponent implements OnInit {
  centerColor = "#c00";
  otherCitiesColor = "#92b605";
  centerRadius = 30;
  otherCitiesRadius = 15;
  baseUrl: string;
  http: HttpClient;
  private eventsSubscription: Subscription;

  @Input() events: Observable<string>;

  public options: any = {

    chart: {
      type: 'networkgraph',
      marginTop: 0,
      style: {
        fontFamily: 'Roboto'
      }
    },

    title: {
      text: ""
    },

    exporting: {
      enabled: false
    },

    tooltip: {
      formatter: function () {
        var info = "";
        switch (this.color) {
          case this.centerColor:
            console.log(this.centerColor);
            info = "is an aiport <b>more than 50</b> direct distinations"
            break;
          case this.otherCitiesColor:
            console.log(this.otherCitiesColor);;
            info = "is an aiport <b>more than 10</b> direct distinations"
            break;
          case this.otherCitiesColor:
            console.log(this.otherCitiesColor);;
            info = "is an aiport <b>less than 10</b> direct distinations"
            break;
        }
        return "<b>" + this.key + "</b>: " + info;
      }
    },

    plotOptions: {
      networkgraph: {
        keys: ['from', 'to'],
        layoutAlgorithm: {
          enableSimulation: true,
          integration: 'verlet',
          linkLength: 100
        }
      }
    },

    series: [{
      marker: {
        radius: 13,
      },
      dataLabels: {
        enabled: true,
        linkFormat: '',
        allowOverlap: true, style: {
          textOutline: false
        }
      },
      data: [],
      nodes:[]
    }]
  }

  getNodes(center) {
    var uniqueCities = new Set<string>();
    this.options.series[0].data.map(x => x.map(y => uniqueCities.add(y)));
    var cityNodes = new Set<Node>();
    for (let city of uniqueCities) {
      if (city !== center) {
        cityNodes.add({ id: city, marker: { radius: this.otherCitiesRadius }, color: this.otherCitiesColor })
      } else {
        cityNodes.add({ id: city, marker: { radius: this.centerRadius }, color: this.centerColor })
      }
    }
    return Array.from(cityNodes);
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.eventsSubscription = this.events.subscribe((center) => this.redrawChart(center));
    this.redrawChart('');
  }

  redrawChart(center) {
    this.http.get<Path[]>(this.baseUrl + 'paths').subscribe(result => {
      this.options.series[0].data = result.map(x => new Array(x.initialCity.name, x.endCity.name));
      this.options.series[0].nodes = this.getNodes(center);
      var chart = Highcharts.chart('container', this.options);
      chart.redraw();
    }, error => console.error(error));
  }

  ngOnDestroy() {
    this.eventsSubscription.unsubscribe();
  }
}
