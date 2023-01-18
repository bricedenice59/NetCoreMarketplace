import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environments } from 'src/environment';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl = environments.baseApiUrl;

  constructor(private http: HttpClient) {}
}
