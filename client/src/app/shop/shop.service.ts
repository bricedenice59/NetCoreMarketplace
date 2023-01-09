import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environments } from 'src/environment';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IProductType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = environments.baseApiUrl;

  constructor(private http: HttpClient) {}

  public getProducts(shopParams: ShopParams): Observable<IPagination> {
    let queryParams = new HttpParams();

    if (shopParams.searchCriteria) {
      queryParams = queryParams.append(
        'searchCriteria',
        shopParams.searchCriteria
      );
    }

    if (shopParams.productTypeId) {
      queryParams = queryParams.append('typeId', shopParams.productTypeId);
    }

    if (shopParams.sortOption) {
      queryParams = queryParams.append('sortBy', shopParams.sortOption);
    }

    if (shopParams.pageIndex) {
      queryParams = queryParams.append('pageIndex', shopParams.pageIndex);
    }

    if (shopParams.pageSize) {
      queryParams = queryParams.append('pageSize', shopParams.pageSize);
    }

    return this.http.get<IPagination>(this.baseUrl + 'products', {
      params: queryParams,
    });
  }

  getProductTypes(): Observable<IProductType> {
    return this.http.get<IProductType>(this.baseUrl + 'products/types');
  }

  getProduct(id: string): Observable<IProduct> {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }
}
