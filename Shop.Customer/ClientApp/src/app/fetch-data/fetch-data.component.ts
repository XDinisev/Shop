import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: Product[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Product[]>(baseUrl + 'product/GetAll').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface Product {
  id: string;
  title: string;
  description: string;
}
