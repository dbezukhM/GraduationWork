import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { HotToastService } from '@ngneat/hot-toast';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private toastService: HotToastService, private httpClient: HttpClient) {}
  showToast() {
    this.httpClient.get<string[]>("https://localhost:7070/WeatherForecast/GetStrings")
    .pipe(
      this.toastService.observe(
        {
          loading: 'Збереження даних',
          success: 'Збереження успішне',
          error: (e) => 'Something did not work, reason: ' + e,
        }
      )
    )
    .subscribe(
      res => {
        console.log(res)
      }
    );
  }
  loadData(){
    this.toastService.loading("asd")
    this.httpClient.get<string[]>("https://localhost:7070/WeatherForecast/GetStrings")
    .subscribe(res => {
      console.log(res);
      this.toastService.close()
    });
  }
  title = 'ClientApp';
}
