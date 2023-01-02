import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPagination } from '../shared/models/pagination';
import { IProductType } from '../shared/models/productType';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:7120/api/';

  constructor(private http: HttpClient) {}

  public getProducts(
    productTypeId?: string,
    sortOption?: string
  ): Observable<IPagination> {
    let queryParams = new HttpParams();

    if (productTypeId) {
      queryParams = queryParams.append('typeId', productTypeId);
    }

    if (sortOption) {
      queryParams = queryParams.append('sortBy', sortOption);
    }

    return this.http.get<IPagination>(this.baseUrl + 'products', {
      params: queryParams,
    });
  }

  getProductTypes(): Observable<IProductType> {
    return this.http.get<IProductType>(this.baseUrl + 'products/types');
  }
}
