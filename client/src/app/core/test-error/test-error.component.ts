import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environments } from 'src/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent implements OnInit {
  baseUrl = environments.baseApiUrl;

  constructor(private httpclient: HttpClient) {}
  ngOnInit(): void {}

  get404Error() {
    this.httpclient
      .get(this.baseUrl + 'products/04cf6f9f-92b7-4a0b-a54b-8b7def4e7c12')
      .subscribe(
        (response: any) => {
          console.log(response);
        },
        (error) => {
          console.error(error);
        }
      );
  }

  get400Error() {
    this.httpclient.get(this.baseUrl + 'buggy/badrequest').subscribe(
      (response: any) => {
        console.log(response);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  get400ValidationError() {
    this.httpclient
      .get(this.baseUrl + 'buggy/badrequest/04cf6f9f4e7c12')
      .subscribe(
        (response) => {
          console.log(response);
        },
        (error) => {
          console.log(error);
        }
      );
  }

  get500Error() {
    this.httpclient.get(this.baseUrl + 'buggy/servererror').subscribe(
      (response: any) => {
        console.log(response);
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
