import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ok } from 'assert';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  fileName = '';

  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.fileName = file.name;
      const formData = new FormData();
      formData.append("file", file);
      const upload$ = this.http.post(this.baseUrl + 'weatherforecast', formData);
      upload$.subscribe(_ => ok, error => alert(error.message));
    }
  }

  onRefreshed(event: any) {
    this.http.get<WeatherForecast[]>(this.baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperature: number;
  summary: string;
}
